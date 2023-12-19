using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] public float currentLife;
    [SerializeField] public float maxLife;
    public bool dead;
    [SerializeField] private ParticleSystem bloodParticles;
    private SpriteRenderer _spriteRenderer;
    private bool _isDamaged;

    [SerializeField] private GameObject damagePopUpPrefab;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetMaxLife(float maxHp)
    {
        maxLife = maxHp;
        currentLife = maxLife;
    }

    public void IncreaseMaxLife(float n)
    {
        SetMaxLife(maxLife + n);
    }

    public void UpdateLife(float damageReceived, bool isCritical)
    {
        if(!dead)
        {
            GameObject feedback = Instantiate(damagePopUpPrefab, transform.position + Vector3.one, Quaternion.identity);
            feedback.GetComponent<DamagePopUp>().SetUp(damageReceived, isCritical);
            currentLife -= damageReceived;
            if (currentLife > maxLife) currentLife = maxLife;
            if (currentLife <= 0)
            {
                currentLife = 0;
                dead = true;
                if (gameObject.CompareTag("Player")) PlayerController.instance.Die();
                else if (gameObject.CompareTag("Enemy")) StartCoroutine(GetComponent<Enemy>().Die());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spell") || other.CompareTag("Ability") && !_isDamaged)
            StartCoroutine(DamageVisualFeedback());
    }

    public IEnumerator DamageVisualFeedback()
    {
        bloodParticles.Play();
        _spriteRenderer.color = Color.red;
        _isDamaged = true;
        yield return new WaitForSeconds(0.25f);
        _isDamaged = false;
        _spriteRenderer.color = Color.white;
    }

    public void ManageHp(float hpToGive)
    {
        currentLife += hpToGive;
        if (currentLife > maxLife) currentLife = maxLife;
        if (currentLife < 0) currentLife = 0;
    }
}