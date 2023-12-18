using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPItem : MonoBehaviour
{
    public int hpAmount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("vida antes" + collision.GetComponent<LifeController>().currentLife);
            collision.GetComponent<LifeController>().ManageHp(hpAmount);
            Debug.Log("vida despues" + collision.GetComponent<LifeController>().currentLife);
            Destroy(gameObject);
        }
    }
}
