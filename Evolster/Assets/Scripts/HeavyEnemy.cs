using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HeavyEnemy : EnemyScript
{
    [SerializeField] float chargingTime;
    [SerializeField] float chargingSpeed;
    Vector2 chargingDirection;
    bool charging;

    public override void Update()
    {
        distance = Vector2.Distance(player.transform.position, transform.position);
        isInRangeAttack = distance <= _stats.attackRange;

        if (!charging) _navMesh.SetDestination(player.position);
        else _navMesh.SetDestination(Vector3.forward * _direction);

        if (isInRangeAttack && Time.time > _lastAttack + attackCooldown) Attack();

    }

    public override void Attack()
    {
        charging = true;
        StartCoroutine(Charge(chargingTime));
    }

    IEnumerator Charge(float timeToWait)
    {
        _navMesh.speed = chargingSpeed;
        Debug.Log("Charging");
        yield return new WaitForSeconds(timeToWait);
        Debug.Log("Charged");
        charging = false;
        _navMesh.speed = _stats.speed;
    }
}
