using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    [SerializeField] float damageBuff;
    [SerializeField] float buffTime;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") BuffsManager.instance.ApplyBuff(damageBuff, buffTime);
        Destroy(gameObject);
    }

}
