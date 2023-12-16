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
    public AudioSource source;

    public bool isInRangeAttack;
    public float distance;

    [SerializeField] GameObject coinPrefab;
    [SerializeField] private ParticleSystem deathParticles;

    public ActorStats enemyStats;
    public bool dead;
    public bool frozen;
    public float currentSpeed;

    public float lastAttack;
    public float attackCooldown;
    public float attackTime;

    [SerializeField] AudioClip[] clips;

    private void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lifeController = GetComponent<LifeController>();
        source = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        navMesh.updateRotation = false;
        navMesh.updateUpAxis = false;
        currentSpeed = enemyStats.movementSpeed;

        PlaySound("Spawn");
        GetComponent<LifeController>().SetMaxLife(enemyStats.maxLife);
    }

    public virtual void Update()
    {
        distance = Vector2.Distance(player.transform.position, transform.position);
        isInRangeAttack = distance <= enemyStats.attackRange;

        if(player.transform.position.x - transform.position.x >= 0) transform.localScale = new Vector3(-1, 1, 1);
        else transform.localScale = new Vector3(1, 1, 1);

        if (!dead)
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
        PlaySound("Attack");
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
    void SpawnBuff()
    {
        int r = Random.Range(0, 100);
        if (r <= 30) BuffsManager.instance.SetSpawnPosition(gameObject.transform.position);
    }

    void SpawnCoin()
    {
        int r = Random.Range(1, 10);
        GameObject newCoin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
        newCoin.GetComponent<Coin>().coinsAmount = r;
    }

    public IEnumerator Die()
    {
        dead = true;
        PlaySound("Die");
        GetComponent<Animator>().SetBool("Dead", true);
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().enabled = false;
        //deathParticles.Play();
        yield return new WaitForSeconds(1f);
        SpawnBuff();
        SpawnCoin();
        Destroy(gameObject);
    }

    public void PlaySound(string soundName)
    {
        AudioClip clipToPlay;
        switch (soundName)
        {
            case "Attack":
                clipToPlay = clips[0];
                break;
            case "Die":
                clipToPlay = clips[1];
                break;
            case "RangeAttack":
                clipToPlay = clips[2];
                break;
            case "Spawn":
                clipToPlay = clips[3];
                break;
            default:
                clipToPlay = clips[0];
                break;
        }
        source.clip = clipToPlay;
        source.PlayOneShot(clipToPlay);
    }
}