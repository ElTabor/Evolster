using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaAbility : MonoBehaviour
{
    [SerializeField] public UniqueAbilityData uniqueAbilityData;

    public float currentDamage;
    public float coolDown;
    private Rigidbody2D _rb;
    public Vector2 direction;

    [SerializeField] private float damageArea;
    private GameObject[] enemies;
    [SerializeField] private LayerMask enemiesLayer;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        currentDamage = uniqueAbilityData.uniqueAbilityDamage;
        coolDown = Time.time;
        _rb.velocity = direction * uniqueAbilityData.uniqueAbilitySpeed;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        damageArea = uniqueAbilityData.uniqueAbilityDamageArea;
    }

    private void Update()
    {
        if (Time.time >= coolDown + uniqueAbilityData.uniqueAbilityLifeTime) 
            Destroy(gameObject);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            DealAreaDamage();
        }
        Destroy(gameObject);
    }

    private void DealAreaDamage()
    {
        Collider2D[] enemiesCollider = Physics2D.OverlapCircleAll(transform.position, damageArea, enemiesLayer);
        foreach (Collider2D enemyColl in enemiesCollider)
        {
            enemyColl.GetComponent<LifeController>().UpdateLife(currentDamage);
        }
    }

}
