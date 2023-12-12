using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{
    public Transform player;

    public Rigidbody2D rb;
    public NavMeshAgent navMesh;
    public Animator animator;
    public LifeController lifeController;

    public bool isInRangeAttack;
    public float distance;

    public ActorStats enemyStats;
    public bool frozen;

    public float lastAttack;
    public float attackCooldown;
    public float attackTime;

    private float currentSpeed;

    private void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lifeController = GetComponent<LifeController>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        navMesh.updateRotation = false;
        navMesh.updateUpAxis = false;
        currentSpeed = enemyStats.movementSpeed;

        GetComponent<LifeController>().SetMaxLife(enemyStats.maxLife);
    }

    public virtual void Update()
    {
        distance = Vector2.Distance(player.transform.position, transform.position);
        isInRangeAttack = distance <= enemyStats.attackRange;

        if(player.transform.position.x - transform.position.x >= 0) transform.localScale = new Vector3(-1, 1, 1);
        else transform.localScale = new Vector3(1, 1, 1);

        if (!lifeController.dead)
        {
            if (isInRangeAttack && Time.time > lastAttack + attackCooldown) Attack();

            //Movement
            if (!isInRangeAttack) navMesh.SetDestination(player.position);
            if (frozen) currentSpeed = 0;
            else currentSpeed = enemyStats.movementSpeed;

            navMesh.speed = currentSpeed;
        }
        else navMesh.isStopped = true;
    }

    protected virtual void Attack()
    {
        player.gameObject.GetComponent<LifeController>().UpdateLife(enemyStats.damage, false);
        lastAttack = Time.time;
        StartCoroutine(AttackTime());
    }

    public IEnumerator AttackTime()
    {
        animator.SetBool("Attacking", true);
        yield return new WaitForSeconds(attackTime);
        animator.SetBool("Attacking", false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, enemyStats.attackRange);
    }
}