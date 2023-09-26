using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class RangeEnemy : EnemyScript
{
    [SerializeField] GameObject spellPrefab;
    [SerializeField] Transform referencePoint;
    Vector2 shootingDirection;

    public void Update()
    {
        base.Update();

        AimToPlayer();
    }

    public override void Attack()
    {
        GameObject spell = Instantiate(spellPrefab, referencePoint.position, Quaternion.identity);
        spell.GetComponent<SpellScript>().direction = shootingDirection;
        _lastAttack = Time.time;
        Debug.Log(spell);
    }

    private void AimToPlayer()
    {
        Vector2 h = player.transform.position - transform.position;
        float distanceToEnemy = Mathf.Sqrt(h.x * h.x + h.y * h.y);
        shootingDirection = h / distanceToEnemy;
        Vector3 newPosition = shootingDirection * 1.5f;
        referencePoint.transform.position = transform.position + newPosition;
    }
}
