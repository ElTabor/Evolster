using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ManaController : MonoBehaviour
{
    public float currentMana;
    float maxMana;
    float lastManaRecover;
    [SerializeField] float manaRecoveryCooldown;

    public void SetMaxMana(float maxMana)
    {
        this.maxMana = maxMana;
        currentMana = maxMana;
    }

    void Update()
    {
        if (Time.time >= lastManaRecover + manaRecoveryCooldown && currentMana < maxMana)
        {
            ManageMana(1);
            lastManaRecover = Time.time;
        }
    }

    public void ManageMana(float newMana)
    {
        currentMana += newMana;
        if (currentMana > maxMana) currentMana = maxMana;
        else if (currentMana < 0) currentMana = 0;
    }
}
