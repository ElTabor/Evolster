using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ExplosionAbilityBullet : AreaAbilityBullet
{
    public override void DealDamage()
    {
        base.DealDamage();
        foreach (Collider2D enemy in Physics2D.OverlapCircleAll(transform.position, damageArea, enemiesLayer))
            enemy.GetComponent<LifeController>().UpdateLife(currentDamage, true);
    }
}
