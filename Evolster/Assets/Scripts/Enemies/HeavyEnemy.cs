using System.Collections;
using UnityEngine;

public class HeavyEnemy : Enemy
{
    [SerializeField] private float chargingTime;
    [SerializeField] private float chargingSpeed;
    private bool _charging;

    void Start()
    {
        base.Start();
        navMesh.stoppingDistance = 0;
    }

    public override void Update()
    {
        base.Update();
        if (!lifeController.dead && !_charging) navMesh.SetDestination(player.position);
    }

    protected override void Attack()
    {
        PlaySound("Attack");
        StartCoroutine(Charge());
        lastAttack = Time.time;
    }

    private IEnumerator Charge()
    {
        animator.SetBool("Attacking", true);
        _charging = true;
        navMesh.speed = chargingSpeed;
        navMesh.SetDestination(player.transform.position + dir*4f);
        float timeToWait = Vector2.Distance(player.transform.position + dir * 4f, transform.position) / chargingSpeed;
        yield return new WaitForSeconds(timeToWait);
        animator.SetBool("Attacking", false);
        _charging = false;
        navMesh.speed = enemyStats.movementSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            collision.GetComponent<LifeController>().UpdateLife(enemyStats.damage, true);
    }
}
