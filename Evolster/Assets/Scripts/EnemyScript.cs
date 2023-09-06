using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent enemy;

    //GameObject player;
    Rigidbody2D rb;

    Vector2 direction;
    [SerializeField] float life;
    [SerializeField] float speed;
    [SerializeField] float damage;
    float lastAttack;
    public float attackCooldown;

    private void Awake()
    {
        enemy = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Start()
    {
        enemy.updateRotation = false;
        enemy.updateUpAxis = false;     
    }

    void Update()
    {
        enemy.SetDestination(player.position);

        bool isOnRangeOfAttack;
        if (Mathf.Abs(player.transform.position.x - transform.position.x) <= 1 && Mathf.Abs(player.transform.position.y - transform.position.y) <= 1f) isOnRangeOfAttack = true;
        else isOnRangeOfAttack = false;

        if (isOnRangeOfAttack)
        {
            direction = Vector2.zero;
            Debug.Log("Colision");
            if (Time.time > lastAttack + attackCooldown)        //Attack cooldown
            {
                lastAttack = Time.time;
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

        rb.velocity = direction * speed;
    }

    void Attack()
    {
        Debug.Log("Attack!");
        PlayerController.instance.UpdateLife(-damage);
    }

    public void GetDamage(float damageReceived)
    {
        life -= damageReceived;
        if (life <= 0) Destroy(gameObject);
    }
}
