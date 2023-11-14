using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    public int currentCoins;

    public void ManageCoins(int coins)
    {
        currentCoins += coins;
    }
}
