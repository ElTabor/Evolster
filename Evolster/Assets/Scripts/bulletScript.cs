using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class BulletScript : MonoBehaviour
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
        Debug.Log(Time.time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}