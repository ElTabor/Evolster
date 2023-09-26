using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public Transform player;
    public NavMeshAgent _enemy;

    //GameObject player;
    public Rigidbody2D _rb;

    public Vector2 _direction;
    public StatsData _stats;
    [SerializeField] private float currentLife;
    public float _lastAttack;
    public float attackCooldown;

    private void Awake()
    {
        _enemy = GetComponent<NavMeshAgent>();
        _rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        _enemy.updateRotation = false;
        _enemy.updateUpAxis = false;
        currentLife = _stats.maxLife;
    }

    public void Update()
    {
        bool isInRangeAttack;
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance <= _stats.attackRange) isInRangeAttack = true;
        else isInRangeAttack = false;

        //if (Mathf.Abs(player.transform.position.x - transform.position.x) <= _stats.attackRange && Mathf.Abs(player.transform.position.y - transform.position.y) <= _stats.attackRange) isInRangeAttack = true;
        //else isInRangeAttack = false;

        if (isInRangeAttack && Time.time > _lastAttack + attackCooldown) Attack();

        //Movement
        _enemy.SetDestination(player.position);
        if(isInRangeAttack) _rb.velocity = _direction * _stats.speed;
    }


    // ReSharper disable Unity.PerformanceAnalysis
    public virtual void Attack()
    {
        Debug.Log("Attack!");
        PlayerController.instance.UpdateLife(-_stats.damage);
        _lastAttack = Time.time;
    }

    public void GetDamage(float damageReceived)
    {
        currentLife -= damageReceived;
        if (currentLife > _stats.maxLife) currentLife = _stats.maxLife;
        else if (currentLife <= 0) Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _stats.attackRange);
    }
}