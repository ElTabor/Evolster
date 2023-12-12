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
    [SerializeField] private ParticleSystem deathParticles;
    [SerializeField] private ParticleSystem bloodParticles;
    private SpriteRenderer _spriteRenderer;
    private bool isDamaged;
    public bool dead;

    [SerializeField] private GameObject damagePopUpPrefab;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetMaxLife(float maxLife)
    {
        this.maxLife = maxLife;
        currentLife = this.maxLife;
    }

    public void IncreaseMaxLife(float n)
    {
        SetMaxLife(maxLife + n);
    }

    public void UpdateLife(float damageReceived, bool isCritical)
    {
        GameObject feedback = Instantiate(damagePopUpPrefab, transform.position + Vector3.one, Quaternion.identity);
        feedback.GetComponent<DamagePopUp>().SetUp(damageReceived, isCritical);
        currentLife -= damageReceived;
        if (currentLife > maxLife) currentLife = maxLife;
        if (currentLife <= 0)
        {
            if (gameObject.CompareTag("Player")) PlayerController.instance.Die();
            else if (gameObject.CompareTag("Enemy"))
            {
                SpawnBuff();
                SpawnCoin();
                StartCoroutine(Die());
            }
            else StartCoroutine(Die());
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
            bloodParticles.Play();
            StartCoroutine(DamageVisualFeedback());
        }
    }

    public IEnumerator DamageVisualFeedback()
    {
        _spriteRenderer.color = Color.red;
        isDamaged = true;
        yield return new WaitForSeconds(0.25f);
        isDamaged = false;
        _spriteRenderer.color = Color.white;
    }

    private IEnumerator Die()
    {
        dead = true;
        GetComponent<Animator>().SetBool("Dead", true);
        GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().enabled = false;
        deathParticles.Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}