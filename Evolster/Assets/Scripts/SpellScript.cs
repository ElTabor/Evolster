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
        Debug.Log(creationTime);
    }

    void Update()
    {
        rb.velocity = direction * spellsData.Speed;

        if (Time.time >= creationTime + spellsData.LifeTime) Destroy(gameObject);
        Debug.Log(Time.time);
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