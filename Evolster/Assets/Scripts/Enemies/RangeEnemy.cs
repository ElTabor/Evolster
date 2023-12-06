using UnityEngine;

public class RangeEnemy : Enemy
{
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private Transform referencePoint;
    Vector2 _shootingDirection;

    public override void Update()
    {
        if (player.transform.position.x - transform.position.x >= 0) transform.localScale = new Vector3(-1, 1, 1);
        else transform.localScale = new Vector3(1, 1, 1);

        float distance = Vector2.Distance(player.transform.position, transform.position);
        isInRangeAttack = distance <= enemyStats.attackRange;

        if(!lifeController.dead)
        {
            //Movement
            if (!isInRangeAttack) navMesh.SetDestination(player.position);
            else navMesh.SetDestination(transform.position);

            if (isInRangeAttack && Time.time > lastAttack + attackCooldown) Attack();
            AimToPlayer();
        }
    }

    protected override void Attack()
    {
        GameObject spell = Instantiate(spellPrefab, referencePoint.position, Quaternion.identity);
        spell.GetComponent<Spell>().direction = _shootingDirection;
        spell.layer = LayerMask.NameToLayer("EnemySpells");
        lastAttack = Time.time;
        StartCoroutine(AttackTime());
    }

    private void AimToPlayer()
    {
        Vector2 h = player.transform.position - transform.position;
        float distanceToEnemy = Mathf.Sqrt(h.x * h.x + h.y * h.y);
        _shootingDirection = h / distanceToEnemy;
        Vector3 newPosition = _shootingDirection * 1.5f;
        referencePoint.transform.position = transform.position + newPosition;
    }
}
