using UnityEngine;
using System.Linq;
using UnityEngine.Profiling.Experimental;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private GameObject player;
    private Rigidbody2D _rb;
    [SerializeField] public StatsData _stats;
    public float currentSpeed;
    public float currentDamage;
    public float currentMana;
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

        currentSpeed = _stats.speed;
        currentDamage = _stats.damage;
        currentMana = _stats.maxMana;
        GetComponent<LifeController>().SetMaxLife(_stats.maxLife);
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
            CastAbility();
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
    
    public void CastAbility()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var aimDirection = (aim.position - transform.position).normalized;
            RaycastHit2D cast = Physics2D.Raycast(transform.position, (aim.position - transform.position), 1000000f);
            GameObject uniqueAbility = Instantiate(uniqueAbilityPrefab, cast.transform.position + aimDirection * 2, Quaternion.identity);
            uniqueAbility.GetComponent<UniqueAbilityScript>().direction = aimDirection;
            uniqueAbility.GetComponent<UniqueAbilityScript>().currentDamage += currentDamage;
            if (cast.collider != null)
            {
                if (cast.collider.gameObject.CompareTag("Enemy"))
                {
                    Debug.Log("cast Enemy!");
                }
            }
            Debug.DrawRay(transform.position, (aim.position - transform.position), Color.red);
            Debug.Log("click");
        }
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