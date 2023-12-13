using System.Collections;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class HeavyEnemy : Enemy
{
    [SerializeField] private float chargingTime;
    [SerializeField] private float chargingSpeed;
    private bool _charging;

    public override void Update()
    {
        base.Update();
        if (!dead && !_charging) navMesh.SetDestination(player.position);
    }

    protected override void Attack() => StartCoroutine(Charge());

    private IEnumerator Charge()
    {
        PlaySound("Attack");
        animator.SetBool("Attacking", true);
        _charging = true;
        navMesh.speed = chargingSpeed;
        navMesh.SetDestination(player.transform.position);
        yield return new WaitForSeconds(chargingTime);
        animator.SetBool("Attacking", false);
        _charging = false;
        navMesh.speed = enemyStats.movementSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Hit Heavy");
            collision.GetComponent<LifeController>().UpdateLife(enemyStats.damage, true);
        }
    }
}
