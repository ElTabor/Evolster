using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    public List<GameObject> spells;
    [SerializeField] GameObject activeSpellPrefab;


    float distanceToNearestEnemy;
    Vector2 shootingDirection;
    bool enemyNearBy;


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
        if (SceneManagerScript.instance.scene != "Lobby" && enemyNearBy) CastSpell();
    }

    public void ChooseSpell(int n)
    {
        if (spells.Count >= n)
            activeSpellPrefab = spells[n];
    }

    private void AimToNearestEnemy()
    {
        Collider2D[] enemiesAround = Physics2D.OverlapCircleAll(transform.position, PlayerController.instance._stats.attackRange);
        GameObject nearestEnemy;

        foreach (Collider2D enemy in enemiesAround)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemyNearBy = true;
                Vector2 h = enemy.transform.position - transform.position;
                float distanceToEnemy = Mathf.Sqrt(h.x * h.x + h.y * h.y);

                if (distanceToNearestEnemy > distanceToEnemy)
                {
                    distanceToNearestEnemy = distanceToEnemy;
                    nearestEnemy = enemy.gameObject;
                }

                shootingDirection = h / distanceToEnemy;

                MoveAimingPoint();
            }
            else enemyNearBy = false;
        }
    }

    private void MoveAimingPoint()
    {
        Vector3 newPosition = shootingDirection * 1.5f;
        shootingPoint.transform.position = transform.position + newPosition;
    }

    private void CastSpell()
    {
        if (Time.time >= _lastAttack + attackCooldown && PlayerController.instance._rb.velocity == Vector2.zero)
        {
            GameObject spell = Instantiate(activeSpellPrefab, shootingPoint.transform.position, shootingPoint.transform.rotation);
            spell.GetComponent<SpellScript>().direction = shootingDirection;
            spell.GetComponent<SpellScript>().currentDamage += PlayerController.instance.currentDamage;
            spell.layer = LayerMask.NameToLayer("FriendlySpells");
            _lastAttack = Time.time;
        }
    }

    public void UnlockNewSpell(GameObject newSpell)
    {
        spells.Add(newSpell);
    }
}
