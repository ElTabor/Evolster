using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private GameObject player;
    private Rigidbody2D _rb;
    [SerializeField] public StatsData _stats;
    int currentLife;

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
    [SerializeField] public GameObject[] availableSpells;
    
    private SpellsData[] spells;
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
        currentLife = _stats.maxLife;

        //foreach (GameObject spell in availableSpells)
        //{
        //    this.spells.Append(GetComponent<SpellScript>().spellsData);
        //}
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
        _rb.MovePosition(_rb.position + _direction * (_stats.speed * Time.fixedDeltaTime));
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
            _lastAttack = Time.time;
        }
    }

    public void UpdateLife(int lifeUpdate)
    {
        currentLife += lifeUpdate;
        if (currentLife > _stats.maxLife) currentLife = _stats.maxLife;
        else if (currentLife <= 0)
        {
            Die();
            UIManager.instance.GameOver();
        }
    }

    private void Die()
    {
        playerGO.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _stats.attackRange);
    }
}