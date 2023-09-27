using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public Transform player;

    public Rigidbody2D _rb;
    public NavMeshAgent _navMesh;

    public Vector2 _direction;
    public bool isInRangeAttack;
    public float distance;

    public StatsData _stats;

    public float _lastAttack;
    public float attackCooldown;

    private void Awake()
    {
        _navMesh = GetComponent<NavMeshAgent>();
        _rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        _navMesh.updateRotation = false;
        _navMesh.updateUpAxis = false;
        _navMesh.speed = _stats.speed;
        GetComponent<LifeController>().SetMaxLife(_stats.maxLife);
    }

    public virtual void Update()
    {
        distance = Vector2.Distance(player.transform.position, transform.position);
        isInRangeAttack = distance <= _stats.attackRange;

        if (isInRangeAttack && Time.time > _lastAttack + attackCooldown) Attack();

        //Movement
        if (!isInRangeAttack) _navMesh.SetDestination(player.position);
        else _navMesh.SetDestination(transform.position);
    }


    // ReSharper disable Unity.PerformanceAnalysis
    public virtual void Attack()
    {
        Debug.Log("Attack!");
        player.gameObject.GetComponent<LifeController>().GetDamage(_stats.damage);
        _lastAttack = Time.time;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _stats.attackRange);
    }
}