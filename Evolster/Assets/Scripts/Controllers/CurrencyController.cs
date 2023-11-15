using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    public int currentCoins;
    GameObject[] coins;

    private void Update()
    {
        CollectCoins();
    }

    public void ManageCoins(int coins)
    {
        currentCoins += coins;
    }

    void CollectCoins()
    {
        coins = GameObject.FindGameObjectsWithTag("Coin");

        foreach (GameObject coin in coins)
        {
            Rigidbody2D rb = coin.GetComponent<Rigidbody2D>();
            if (Vector2.Distance(coin.transform.position, transform.position) <= 5f)
                rb.velocity = (transform.position - coin.transform.position).normalized * 10f;
            else rb.velocity = Vector2.zero;
        }

    }
}
