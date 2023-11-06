using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    public Transform player;

    public Rigidbody2D rb;
    public NavMeshAgent navMesh;

    public Vector2 direction;
    public bool isInRangeAttack;
    public float distance;

    [SerializeField] protected ActorStats enemyStats;

    public float lastAttack;
    public float attackCooldown;

    private void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        navMesh.updateRotation = false;
        navMesh.updateUpAxis = false;
        navMesh.speed = enemyStats.movementSpeed;
        GetComponent<LifeController>().SetMaxLife(enemyStats.maxLife);
    }

    public virtual void Update()
    {
        distance = Vector2.Distance(player.transform.position, transform.position);
        isInRangeAttack = distance <= enemyStats.attackRange;

        if (isInRangeAttack && Time.time > lastAttack + attackCooldown) Attack();

        //Movement
        if (!isInRangeAttack) navMesh.SetDestination(player.position);
        else navMesh.SetDestination(transform.position);
    }

    protected virtual void Attack()
    {
        player.gameObject.GetComponent<LifeController>().UpdateLife(enemyStats.damage);
        lastAttack = Time.time;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, enemyStats.attackRange);
    }
}