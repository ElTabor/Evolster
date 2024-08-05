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
            collision.GetComponent<ManaController>().ManageMana(manaAmount);
            Destroy(gameObject);
        }
    }
}
