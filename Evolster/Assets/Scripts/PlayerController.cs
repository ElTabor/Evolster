using UnityEngine;
using System.Linq;
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
    //[SerializeField] public GameObject[] availableSpells;
    public List<GameObject> availableSpells;
    
    [SerializeField] public GameObject gameOverScreen;

    [SerializeField] GameObject playerGO;

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
        if (SceneManagerScript.instance.scene != "Lobby" && enemyNearBy) Attack();
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

    private void Attack()
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

    public void Die()
    {
        UIManager.instance.GameOver();
        GetComponent<LifeController>().UpdateLife(-GetComponent<LifeController>()._maxLife);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _stats.attackRange);
    }
}