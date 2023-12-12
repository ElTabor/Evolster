using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceAreaAbilityBullet : AreaAbilityBullet
{
    private Collider2D[] frozenEnemies;
    public override void DealDamage()
    {
        base.DealDamage();
        frozenEnemies = Physics2D.OverlapCircleAll(transform.position, damageArea, enemiesLayer);
        StartCoroutine(FreezeEnemies());
    }

    IEnumerator FreezeEnemies()
    {
        foreach (Collider2D enemy in frozenEnemies)
        {
            enemy.GetComponent<Enemy>().frozen = true;
            enemy.GetComponent<SpriteRenderer>().color = Color.cyan;
            enemy.GetComponent<LifeController>().UpdateLife(currentDamage, true);
        }

        yield return new WaitForSeconds(1f);

        GetComponent<SpriteRenderer>().enabled = false;

        yield return new WaitForSeconds(1f);

        foreach (Collider2D enemy in frozenEnemies)
        {
            if (enemy != null)
            {
                enemy.GetComponent<Enemy>().frozen = false;
                enemy.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
}
