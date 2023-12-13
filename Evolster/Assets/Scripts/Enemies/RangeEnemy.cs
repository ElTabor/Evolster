using UnityEngine;

public class RangeEnemy : Enemy
{
    [SerializeField] private GameObject spellPrefab;
    [SerializeField] private Transform referencePoint;
    Vector2 _shootingDirection;

    public override void Update()
    {
        base.Update();
        if(!dead)
        {
            AimToPlayer();
            if(isInRangeAttack && Time.time > lastAttack + attackCooldown)
            {
                navMesh.isStopped = true;
                animator.SetBool("Attacking", true);
            }
            else navMesh.isStopped = false;
        }
    }

    protected override void Attack()
    {
        //PlaySound("RangeAttack");
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
