using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] public float currentLife;
    [SerializeField] public float maxLife;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] private Material original, damaged;
    private bool isDamaged;

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
        UIManager.instance.DamageFeedback(transform, damageReceived);
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
        int r = UnityEngine.Random.Range(0, 100);
        if (r <= 30) BuffsManager.instance.SetSpawnPosition(gameObject.transform.position);
    }

    void SpawnCoin()
    {
        int r = UnityEngine.Random.Range(1, 10);
        GameObject newCoin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
        newCoin.GetComponent<Coin>().coinsAmount = r;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spell") || other.CompareTag("Ability") && !isDamaged)
        {
            StartCoroutine(DamageVisualFeedback());
        }
    }

    public IEnumerator DamageVisualFeedback()
    {
        GetComponent<SpriteRenderer>().material = GetComponent<LifeController>().damaged;
        isDamaged = true;
        yield return new WaitForSeconds(0.25f);
        isDamaged = false;
        GetComponent<SpriteRenderer>().material = GetComponent<LifeController>().original;
    }
}