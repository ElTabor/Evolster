using UnityEngine;
using System.Linq;
using UnityEngine.Profiling.Experimental;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    LifeController lifeController;
    public ManaController manaController;

    [SerializeField] private GameObject player;
    private Rigidbody2D _rb;
    [SerializeField] public StatsData _stats;
    public float currentSpeed;
    public float currentDamage;
    public bool isBuffed;

    float distanceToNearestEnemy;
    Vector2 shootingDirection;
    bool enemyNearBy;

    private float _horizontal;
    private float _vertical;
    private Vector2 _direction;


    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject shootingPoint;
    private float _lastAttack;
    [SerializeField] private float attackCooldown;


    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private GameObject uniqueAbilityPrefab;
    [SerializeField] public List<GameObject> availableSpells;
    
    [SerializeField] public GameObject gameOverScreen;

    [SerializeField] GameObject playerGO;

    [SerializeField] private bool uniqueAbilityIsAvailable = false;
    [SerializeField] private Transform aim;

    public PlayerController(float speed, GameObject player, float horizontal, GameObject shootingPoint)
    {
        _stats.speed = speed;
        this.player = player;
        _horizontal = horizontal;
        this.shootingPoint = shootingPoint;
    }

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        player = GameObject.FindGameObjectWithTag("Player");
        _rb = player.GetComponent<Rigidbody2D>();
        lifeController = GetComponent<LifeController>();
        manaController = GetComponent<ManaController>();

        currentSpeed = _stats.speed;
        currentDamage = _stats.damage;
        lifeController.SetMaxLife(_stats.maxLife);
        manaController.SetMaxMana(_stats.maxMana);
    }

    void Update()
    {
        GetInput();

        #region Movement

        _direction = new Vector2(_horizontal, _vertical).normalized;

        #endregion

        AimToNearestEnemy();        //Automaticlly shoot to the nearest enemy only when the player is steady

                //Shooting
        if (SceneManagerScript.instance.scene != "Lobby" && enemyNearBy) CastSpell();

        if (uniqueAbilityIsAvailable)
        {
            aim.position = Camera.main.ScreenToWorldPoint(new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                -Camera.main.transform.position.z));
        }
    }

    private void AimToNearestEnemy()
    {
        Collider2D[] enemiesAround = Physics2D.OverlapCircleAll(transform.position, _stats.attackRange);
        GameObject nearestEnemy;

        foreach (Collider2D enemy in enemiesAround)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemyNearBy = true;
                Vector2 h = enemy.transform.position - transform.position;
                float distanceToEnemy = Mathf.Sqrt(h.x * h.x + h.y * h.y);

                if (distanceToNearestEnemy > distanceToEnemy)
                {
                    distanceToNearestEnemy = distanceToEnemy;
                    nearestEnemy = enemy.gameObject;
                }

                shootingDirection = h / distanceToEnemy;

                MoveAimingPoint();
            }
            else enemyNearBy = false;
        }
    }

    private void MoveAimingPoint()
    {
        Vector3 newPosition = shootingDirection * 1.5f;
        shootingPoint.transform.position = transform.position + newPosition;
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _direction * (currentSpeed * Time.fixedDeltaTime));
    }

    private void GetInput()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0)) TryCastAbility(uniqueAbilityPrefab.GetComponent<UniqueAbilityScript>());
    }

    private void CastSpell()
    {
        if (Time.time >= _lastAttack + attackCooldown && _rb.velocity == Vector2.zero)
        {
            GameObject spell = Instantiate(spellPrefab, shootingPoint.transform.position, gun.transform.rotation);
            spell.GetComponent<SpellScript>().direction = shootingDirection;
            spell.GetComponent<SpellScript>().currentDamage += currentDamage;
            spell.layer = LayerMask.NameToLayer("FriendlySpells");
            _lastAttack = Time.time;
        }
    }
    
    public void TryCastAbility(UniqueAbilityScript abilityToCast)
    {
        if (manaController.currentMana >= abilityToCast.uniqueAbilityData.manaCost) CastAbility();
        Debug.Log("Tried to cast: " + abilityToCast.uniqueAbilityData.uniqueAbilityName);
    }

    public void CastAbility()
    {
        var aimDirection = (aim.position - transform.position).normalized;
        RaycastHit2D cast = Physics2D.Raycast(transform.position, (aim.position - transform.position), 1000000f);
        GameObject uniqueAbility = Instantiate(uniqueAbilityPrefab, cast.transform.position + aimDirection * 2, Quaternion.identity);
        uniqueAbility.GetComponent<UniqueAbilityScript>().direction = aimDirection;
        uniqueAbility.GetComponent<UniqueAbilityScript>().currentDamage += currentDamage;
        manaController.ManageMana(-uniqueAbility.GetComponent<UniqueAbilityScript>().uniqueAbilityData.manaCost);

        //Debugs
        if (cast.collider != null && cast.collider.gameObject.CompareTag("Enemy")) Debug.Log("cast Enemy!");
        Debug.DrawRay(transform.position, (aim.position - transform.position), Color.red);
        Debug.Log("Ability Cast");
    }

    public void Die()
    {
        UIManager.instance.GameOver();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _stats.attackRange);
    }
}