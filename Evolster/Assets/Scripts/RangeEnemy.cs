using UnityEngine;

public class RangeEnemy : EnemyScript
{
    [SerializeField] GameObject spellPrefab;
    [SerializeField] Transform referencePoint;
    Vector2 shootingDirection;

    public override void Update()
    {
        bool isInRangeAttack;
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance <= _stats.attackRange) isInRangeAttack = true;
        else isInRangeAttack = false;

        if (isInRangeAttack && Time.time > _lastAttack + attackCooldown) Attack();

        //Movement
        if (!isInRangeAttack) _navMesh.SetDestination(player.position);
        else _navMesh.SetDestination(transform.position);

        AimToPlayer();
    }

    public override void Attack()
    {
        GameObject spell = Instantiate(spellPrefab, referencePoint.position, Quaternion.identity);
        spell.GetComponent<SpellScript>().direction = shootingDirection;
        spell.layer = LayerMask.NameToLayer("EnemySpells");
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
