using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SpellScript : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] int damage;
    public Vector2 direction;

    float creationTime;
    [SerializeField] float lifeTime;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        creationTime = Time.time;
        Debug.Log(creationTime);
    }
    void Update()
    {
        rb.velocity = direction * speed;

        if (Time.time >= creationTime + lifeTime) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyScript>().GetDamage(damage);
        }
        Destroy(gameObject);
    }
}