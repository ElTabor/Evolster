using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    public List<GameObject> spells;
    [SerializeField] private GameObject activeSpellPrefab;


    private float _distanceToNearestEnemy;
    private Vector2 _shootingDirection;
    private bool _enemyNearBy;


    private float _lastAttack;
    public GameObject shootingPoint;
    [SerializeField] private float attackCooldown;

    private void Start()
    {
        activeSpellPrefab = spells[0];
    }
    void Update()
    {
        //Automaticlly aim to the nearest enemy
        AimToNearestEnemy();

        //Shoot
        if (GameManager.instance.onLevel && _enemyNearBy) CastSpell();
    }

    public void ChooseSpell(int n)
    {
        if (spells.Count >= n)
            activeSpellPrefab = spells[n];
    }

    private void AimToNearestEnemy()
    {
        Collider2D[] enemiesAround = Physics2D.OverlapCircleAll(transform.position, PlayerController.instance.playerStats.attackRange);
        GameObject nearestEnemy;

        foreach (Collider2D enemy in enemiesAround)
        {
            if (enemy.CompareTag("Enemy"))
            {
                _enemyNearBy = true;
                Vector2 h = enemy.transform.position - transform.position;
                float distanceToEnemy = Mathf.Sqrt(h.x * h.x + h.y * h.y);

                if (_distanceToNearestEnemy > distanceToEnemy)
                {
                    _distanceToNearestEnemy = distanceToEnemy;
                    nearestEnemy = enemy.gameObject;
                }

                _shootingDirection = h / distanceToEnemy;

                MoveAimingPoint();
            }
            else _enemyNearBy = false;
        }
    }

    private void MoveAimingPoint()
    {
        Vector3 newPosition = _shootingDirection * 1.5f;
        shootingPoint.transform.position = transform.position + newPosition;
    }

    private void CastSpell()
    {
        if (Time.time >= _lastAttack + attackCooldown && PlayerController.instance.rb.velocity == Vector2.zero && !GameManager.instance.gamePaused)
        {
            GameObject spell = Instantiate(activeSpellPrefab, shootingPoint.transform.position, shootingPoint.transform.rotation);
            spell.GetComponent<Spell>().direction = _shootingDirection;
            spell.GetComponent<Spell>().currentDamage += PlayerController.instance.currentDamage;
            spell.layer = LayerMask.NameToLayer("FriendlySpells");
            _lastAttack = Time.time;
        }
    }

    public void UnlockNewSpell(GameObject newSpell)
    {
        spells.Add(newSpell);
    }
}
