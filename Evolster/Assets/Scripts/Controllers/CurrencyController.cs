using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyController : MonoBehaviour
{
    public int currentCoins;
    private TextMeshProUGUI feedbackText;
    [SerializeField] float feedbackTime;
    private float startTime;
    GameObject[] coins;
    public bool showingFeedbackText;

    private void Update()
    {
        CollectCoins();
    }

    public void ManageCoins(int coins) => currentCoins += coins;

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

    public void ShowFeedback(int coins)
    {
        UIManager.instance.lastFeedbackTime = Time.time;
        UIManager.instance.coinsToShow += coins;
    }
}
