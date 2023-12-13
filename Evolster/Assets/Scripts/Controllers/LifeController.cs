using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] public float currentLife;
    [SerializeField] public float maxLife;
    [SerializeField] private Material original, damaged;
    [SerializeField] private ParticleSystem bloodParticles;
    private SpriteRenderer _spriteRenderer;
    private bool isDamaged;

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
            else if (gameObject.CompareTag("Enemy")) StartCoroutine(GetComponent<Enemy>().Die());
        }
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
}