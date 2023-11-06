using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniqueAbility : MonoBehaviour
{
    [SerializeField] public UniqueAbilityData uniqueAbilityData;

    public float currentDamage;
    public float coolDown;
    private Rigidbody2D _rb;
    public Vector2 direction;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        currentDamage = uniqueAbilityData.uniqueAbilityDamage;
        coolDown = Time.time;
        _rb.velocity = direction * uniqueAbilityData.uniqueAbilitySpeed;
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
            collision.GetComponent<LifeController>().UpdateLife(currentDamage);
        }
        Destroy(gameObject);
    }
}
