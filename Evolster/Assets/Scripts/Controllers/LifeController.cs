using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{
    [SerializeField] public float currentLife;
    [SerializeField] public float maxLife;
    [SerializeField] GameObject coinPrefab;

    public void SetMaxLife(float maxLife)
    {
        this.maxLife = maxLife;
        currentLife = this.maxLife;
    }

    public void IncreaseMaxLife(float n)
    {
        SetMaxLife(maxLife + n);
    }

    public void UpdateLife(float damageReceived)
    {
        currentLife -= damageReceived;
        if (currentLife > maxLife) currentLife = maxLife;
        if (currentLife <= 0)
        {
            if (gameObject.CompareTag("Player")) PlayerController.instance.Die();
            else if (gameObject.CompareTag("Enemy"))
            {
                SpawnBuff();
                SpawnCoin();
                Destroy(gameObject);
            }
            else Destroy(gameObject);
        }
    }

    void SpawnBuff()
    {
        int r = Random.Range(0, 100);
        if (r <= 30) BuffsManager.instance.SetSpawnPosition(gameObject.transform.position);
    }

    void SpawnCoin()
    {
        int r = Random.Range(1, 10);
        GameObject newCoin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
        newCoin.GetComponent<Coin>().coinsAmount = r;
    }
}