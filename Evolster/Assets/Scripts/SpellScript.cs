using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpellScript : MonoBehaviour
{
    [SerializeField] SpellsData spellsData;

    public Vector2 direction;

    float creationTime;
    
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        creationTime = Time.time;
        rb.velocity = direction * spellsData.Speed;
    }

    void Update()
    {
        if (Time.time >= creationTime + spellsData.LifeTime) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyScript>().GetDamage(spellsData.Damage);
        }
        Destroy(gameObject);
    }
}