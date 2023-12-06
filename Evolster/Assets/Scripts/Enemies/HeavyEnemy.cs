using System.Collections;
using UnityEngine;

public class HeavyEnemy : Enemy
{
    [SerializeField] private float chargingTime;
    [SerializeField] private float chargingSpeed;
    private bool _charging;

    public override void Update()
    {
        if (player.transform.position.x - transform.position.x >= 0) transform.localScale = new Vector3(-1, 1, 1);
        else transform.localScale = new Vector3(1, 1, 1);

        distance = Vector2.Distance(player.transform.position, transform.position);
        isInRangeAttack = distance <= enemyStats.attackRange;

        if(!lifeController.dead)
        {
            if (!_charging) navMesh.SetDestination(player.position);

            if (isInRangeAttack && Time.time > lastAttack + attackCooldown) Attack();
        }
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
