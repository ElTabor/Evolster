using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaItem : MonoBehaviour
{
    public int manaAmount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("mana antes" + collision.GetComponent<ManaController>().currentMana);
            collision.GetComponent<ManaController>().ManageMana(manaAmount);
            Debug.Log("mana despues" + collision.GetComponent<ManaController>().currentMana);
            Destroy(gameObject);
        }
    }
}
