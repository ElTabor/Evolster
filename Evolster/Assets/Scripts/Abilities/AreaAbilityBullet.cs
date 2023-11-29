using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AreaAbilityBullet : MonoBehaviour
{
    public float damageArea;
    public float currentDamage;
    private Rigidbody2D _rb;
    public Vector2 direction;
    public float speed;
    public float lifeTime;
    private float startTime;
    Animator animator;
    [SerializeField] LayerMask enemiesLayer;
    private bool exploding;

    private void Start()
    {
        animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time >= startTime + lifeTime)
            Destroy(gameObject);

        animator.SetBool("Exploding", exploding);

        if (!exploding) _rb.velocity = direction * speed;
        else _rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D() => DealAreaDamage();

    private void DealAreaDamage()
    {
        exploding = true;
        foreach (Collider2D enemy in Physics2D.OverlapCircleAll(transform.position, damageArea, enemiesLayer))
            enemy.GetComponent<LifeController>().UpdateLife(currentDamage);
        Debug.Log(Physics2D.OverlapCircleAll(transform.position, damageArea, enemiesLayer).Count());
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, damageArea);
    }
}
