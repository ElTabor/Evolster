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
        if (!lifeController.dead && !_charging) navMesh.SetDestination(player.position);
        if (player.transform.position.x - transform.position.x >= 0) transform.localScale = new Vector3(-1, 1, 1);
        else transform.localScale = new Vector3(1, 1, 1);

        distance = Vector2.Distance(player.transform.position, transform.position);
        isInRangeAttack = distance <= enemyStats.attackRange;

        if(!lifeController.dead)
        {
            if (!_charging) navMesh.SetDestination(player.position);

            if (isInRangeAttack && Time.time > lastAttack + attackCooldown)
            {
                animator.SetBool("Attacking", true);
                Attack();
            }
        }
    }

    protected override void Attack()
    {
        StartCoroutine(Charge());
    }

    private IEnumerator Charge()
    {
        //animator.SetBool("Charging", false);
        animator.SetBool("Attacking", true);
        _charging = true;
        navMesh.speed = chargingSpeed;
        navMesh.acceleration = 20;
        navMesh.SetDestination(player.transform.position);
        yield return new WaitForSeconds(chargingTime);
        animator.SetBool("Attacking", false);
        _charging = false;
        navMesh.acceleration = 8;
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
