using System.Collections;
using UnityEngine;

public class HeavyEnemy : Enemy
{
    [SerializeField] private float chargingTime;
    [SerializeField] private float chargingSpeed;
    private bool _charging;

    public override void Update()
    {
        base.Update();
        if (!lifeController.dead && !_charging) navMesh.SetDestination(player.position);
    }

    protected override void Attack()
    {
        StartCoroutine(Charge());
        StartCoroutine(AttackTime());
    }

    private IEnumerator Charge()
    {
        _charging = true;
        navMesh.speed = chargingSpeed;
        navMesh.SetDestination(player.transform.position);
        yield return new WaitForSeconds(chargingTime);
        _charging = false;
        navMesh.speed = enemyStats.movementSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<LifeController>().UpdateLife(enemyStats.damage, true);
        }
    }
}
