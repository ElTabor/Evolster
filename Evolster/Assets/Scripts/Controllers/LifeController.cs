using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{
    [SerializeField] public float currentLife;
    [SerializeField] public float maxLife;

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
            if (gameObject.CompareTag("Player")) PlayerController.Instance.Die();
            else if (gameObject.CompareTag("Enemy"))
            {
                int r = Random.Range(0, 100);
                if(r <= 30) BuffsManager.instance.SetSpawnPosition(gameObject.transform.position);
                Destroy(gameObject);
            }
            else Destroy(gameObject);
        }
    }
}