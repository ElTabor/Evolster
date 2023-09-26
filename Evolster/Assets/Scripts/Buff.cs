using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    [SerializeField] float damageBuff;
    [SerializeField] float buffTime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") ApplyBuff();
    }

    void ApplyBuff()
    {
        PlayerController.instance.currentDamage += damageBuff;
        Destroy(gameObject);
    }

    void RemoveBuff()
    {
        PlayerController.instance.currentDamage -= damageBuff;
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown()
    {
        yield return new WaitForSeconds(buffTime);
        RemoveBuff();
    }
}
