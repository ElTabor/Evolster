using System.Collections;
using UnityEngine;
using UnityEngine.AI;

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
    public Vector3 dir;

    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject manaPrefab;
    [SerializeField] private GameObject hpPrefab;
    [SerializeField] private ParticleSystem deathParticles;

    public ActorStats enemyStats;
    public bool frozen;
    public bool electrocuted;
    public float currentSpeed;

    public float lastAttack;
    public float attackCooldown;
    public float attackTime;

    [SerializeField] private AudioClip[] clips;

    private void Awake()
    {
        navMesh = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lifeController = GetComponent<LifeController>();
        source = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void Start()
    {
        navMesh.updateRotation = false;
        navMesh.updateUpAxis = false;
        navMesh.stoppingDistance = enemyStats.attackRange;
        currentSpeed = enemyStats.movementSpeed;

        PlaySound("Spawn");
        lifeController.SetMaxLife(enemyStats.maxLife);
    }

    public virtual void Update()
    {
        dir = (player.transform.position - transform.position).normalized;
        distance = Vector2.Distance(player.transform.position, transform.position);
        isInRangeAttack = distance <= enemyStats.attackRange;

        if (player.transform.position.x - transform.position.x >= 0) transform.localScale = new Vector3(-1, 1, 1);
        else transform.localScale = new Vector3(1, 1, 1);

        if (!lifeController.dead)
        {
            //Movement
            if (!isInRangeAttack) navMesh.SetDestination(player.position);
            else if (Time.time > lastAttack + attackCooldown) Attack();

            if (frozen) currentSpeed = 0;
            else if (electrocuted) currentSpeed = enemyStats.movementSpeed / 2;
            else currentSpeed = enemyStats.movementSpeed;

            navMesh.speed = currentSpeed;
        }
        else navMesh.isStopped = true;
    }

    protected virtual void Attack()
    {
        PlaySound("Attack");
        lastAttack = Time.time;
        StartCoroutine(AttackTime());
        Collider2D[] inRangeCols = Physics2D.OverlapCircleAll(transform.position + dir * enemyStats.attackRange, enemyStats.attackRange);
        foreach(Collider2D col in inRangeCols)
            player.gameObject.GetComponent<LifeController>().UpdateLife(enemyStats.damage, false);
    }

    public IEnumerator AttackTime()
    {
        animator.SetBool("Attacking", true);
        yield return new WaitForSeconds(attackTime);
        animator.SetBool("Attacking", false);
    }

    private void SpawnBuff()
    {
        Vector3 position = transform.position + new Vector3(Random.value, Random.value) * Random.value * 3;
        int r = Random.Range(0, 100);
        if (r <= 30)
        {
            BuffsManager.instance.SetSpawnPosition(position);
            Debug.Log(position + "Buff");
        }
        PlaySound("Item");
    }
        
    private void SpawnCoin()
    {
        Vector3 position = transform.position + new Vector3(Random.value, Random.value) * Random.value * 3;
        int r = Random.Range(1, 10);
        GameObject newCoin = Instantiate(coinPrefab, position, Quaternion.identity);
        newCoin.GetComponent<Coin>().coinsAmount = r;
        PlaySound("Item");
        Debug.Log(position + "Coin");
    }

    private void SpawnManaItem()
    {
        Vector3 position = transform.position + new Vector3(Random.value, Random.value) * Random.value * 3;
        int r = Random.Range(1, 10);
        if(r <= 3)
        {
            Instantiate(manaPrefab, position, Quaternion.identity);
            Debug.Log(position + "Mana");
        }
        PlaySound("Item");
    }
    
    private void SpawnHpItem()
    {
        Vector3 position = transform.position + new Vector3(Random.value, Random.value) * Random.value * 3;
        int r = Random.Range(1, 10);
        if(r <= 1)
        {
            Instantiate(hpPrefab, position, Quaternion.identity);
            Debug.Log(position + "HP");
        }
        PlaySound("Item");
    }

    public IEnumerator Electrocute(float timeToWait)
    {
        electrocuted = true;
        yield return new WaitForSeconds(timeToWait);
        electrocuted = false;
    }

    public IEnumerator Die()
    {
        PlaySound("Die");
        GetComponent<Animator>().SetBool("Dead", true);
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().enabled = false;
        SpawnBuff();
        SpawnCoin();
        SpawnManaItem();
        SpawnHpItem();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    protected void PlaySound(string soundName)
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
            case "Item":
                clipToPlay = clips[4];
                break;
            default:
                clipToPlay = clips[0];
                break;
        }
        source.clip = clipToPlay;
        source.PlayOneShot(clipToPlay);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, enemyStats.attackRange);
        Gizmos.DrawWireSphere(transform.position + dir * enemyStats.attackRange, enemyStats.attackRange);
    }
}