using System.Collections;
using UnityEngine;

public class HeavyEnemy : Enemy
{
    [SerializeField] private float chargingTime;
    [SerializeField] private float chargingSpeed;
    private bool _charging;

    public override void Update()
    {
        distance = Vector2.Distance(player.transform.position, transform.position);
        isInRangeAttack = distance <= enemyStats.attackRange;

        if (!_charging) navMesh.SetDestination(player.position);
        else navMesh.SetDestination(Vector3.forward * direction);

        if (isInRangeAttack && Time.time > lastAttack + attackCooldown) Attack();
    }

    protected override void Attack()
    {
        _charging = true;
        StartCoroutine(Charge(chargingTime));
    }

    private IEnumerator Charge(float timeToWait)
    {
        navMesh.speed = chargingSpeed;
        Debug.Log("Charging");
        yield return new WaitForSeconds(timeToWait);
        Debug.Log("Charged");
        _charging = false;
        navMesh.speed = enemyStats.movementSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<LifeController>().UpdateLife(enemyStats.damage);
        }
    }
}
