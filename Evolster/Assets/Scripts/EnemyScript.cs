using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent _enemy;

    //GameObject player;
    private Rigidbody2D _rb;

    private Vector2 _direction;
    [SerializeField] private float life;
    [SerializeField] private float speed;
    [SerializeField] private float damage;
    private float _lastAttack;
    public float attackCooldown;

    private void Awake()
    {
        _enemy = GetComponent<NavMeshAgent>();
        _rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Start()
    {
        Debug.Log("Collision");
        _enemy.updateRotation = false;
        _enemy.updateUpAxis = false;     
    }

    private void Update()
    {
        _enemy.SetDestination(player.position);

        bool isOnRangeOfAttack;
        if (Mathf.Abs(player.transform.position.x - transform.position.x) <= 1 && Mathf.Abs(player.transform.position.y - transform.position.y) <= 1f) isOnRangeOfAttack = true;
        else isOnRangeOfAttack = false;

        if (isOnRangeOfAttack)
        {
            if (Time.time > _lastAttack + attackCooldown)        //Attack cooldown
            {
                _lastAttack = Time.time;
                Attack();
            }
        }
        //else
        //{
        //    if (player.transform.position.x < transform.position.x) direction = new Vector2(-1, direction.y);
        //    else direction = new Vector2(1, direction.y);
        //    if (player.transform.position.y < transform.position.y) direction = new Vector2(direction.x, -1);
        //    else direction = new Vector2(direction.x, 1);
        //}

        //MOVEMENT

        _rb.velocity = _direction * speed;
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Attack()
    {
        Debug.Log("Attack!");
        PlayerController.Instance.UpdateLife(-damage);
    }

    public void GetDamage(float damageReceived)
    {
        life -= damageReceived;
        if (life <= 0) Destroy(gameObject);
    }
}
