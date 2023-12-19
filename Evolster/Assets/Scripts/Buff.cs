using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour
{
    [SerializeField] private float damageBuff;
    [SerializeField] private float buffTime;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            BuffsManager.instance.ApplyBuff(damageBuff, buffTime);
            Destroy(gameObject);
        }
    }
}
