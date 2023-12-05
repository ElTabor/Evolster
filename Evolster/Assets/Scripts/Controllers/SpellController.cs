using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellController : MonoBehaviour
{
    public List<GameObject> spells;
    [SerializeField] private GameObject activeSpellPrefab;

    [SerializeField] Collider2D[] enemiesOnScreen;
    [SerializeField] float _distanceToNearestEnemy;
    private Vector2 _shootingDirection;
    private bool _enemyNearBy;
    [SerializeField]GameObject nearestEnemy;
    [SerializeField] LayerMask enemiesLayer;


    private float _lastAttack;
    public GameObject shootingPoint;
    [SerializeField] private float attackCooldown;

    private void Start()
    {
        activeSpellPrefab = spells[0];
        _distanceToNearestEnemy = float.MaxValue;
        nearestEnemy = null;
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
        //Find all enemies
        enemiesOnScreen = Physics2D.OverlapCircleAll(PlayerController.instance.transform.position, PlayerController.instance.playerStats.attackRange, enemiesLayer);

        //Find nearest enemy
        foreach (Collider2D enemy in enemiesOnScreen)
        {
            float distanceToEnemy = Vector2.Distance(PlayerController.instance.transform.position, enemy.transform.position);
            if(nearestEnemy == null || distanceToEnemy < _distanceToNearestEnemy)
                nearestEnemy = enemy.gameObject;
        }
                //Aim to nearest enemty
        if (nearestEnemy != null)
        {
            _shootingDirection = (nearestEnemy.transform.position - PlayerController.instance.transform.position).normalized;
            MoveAimingPoint();
            _distanceToNearestEnemy = (nearestEnemy.transform.position - PlayerController.instance.transform.position).magnitude;
            _enemyNearBy = _distanceToNearestEnemy < PlayerController.instance.playerStats.attackRange;
        }
    }

    private void MoveAimingPoint()
    {
        Vector3 newPosition = _shootingDirection * 1.5f;
        shootingPoint.transform.position = PlayerController.instance.transform.position + newPosition;
    }

    private void CastSpell()
    {
        if (Time.time >= _lastAttack + attackCooldown && PlayerController.instance.rb.velocity == Vector2.zero && !GameManager.instance.gamePaused)
        {
            GameObject spell = Instantiate(activeSpellPrefab, shootingPoint.transform.position, shootingPoint.transform.rotation);
            float shootAngle = Mathf.Atan2(_shootingDirection.y, _shootingDirection.x) * Mathf.Rad2Deg;
            spell.transform.rotation = Quaternion.AngleAxis(shootAngle, Vector3.forward);
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
