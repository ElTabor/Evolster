using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueAbilityScript : MonoBehaviour
{
    [SerializeField] public UniqueAbilityData uniqueAbilityData;

    public float currentDamage;
    public float coolDown;
    Rigidbody2D rb;
    public Vector2 direction;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentDamage = uniqueAbilityData.uniqueAbilityDamage;
        coolDown = Time.time;
        rb.velocity = direction * uniqueAbilityData.uniqueAbilitySpeed;
    }
    
    void Update()
    {
        if (Time.time >= coolDown + uniqueAbilityData.uniqueAbilityLifeTime) 
            Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<LifeController>().GetDamage(currentDamage);
            Debug.Log("Collision ability");
        }
        Destroy(gameObject);
    }
}
