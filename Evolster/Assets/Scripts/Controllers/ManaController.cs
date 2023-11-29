using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ManaController : MonoBehaviour
{
    public float currentMana;
    private float _maxMana;
    private float _lastManaRecover;
    [SerializeField] private float manaRecoveryCooldown;

    public void SetMaxMana(float maxMana)
    {
        _maxMana = maxMana;
        currentMana = maxMana;
    }

    void Update()
    {
        if (Time.time >= _lastManaRecover + manaRecoveryCooldown && currentMana < _maxMana)
        {
            ManageMana(1);
            _lastManaRecover = Time.time;
        }
    }

    public bool ManageAbilityAvailability()
    {
        return currentMana >= PlayerController.instance.abilityController.ability.abilityData.manaCost;
    }

    public void ManageMana(float newMana)
    {
        currentMana += newMana;
        if (currentMana > _maxMana) currentMana = _maxMana;
        else if (currentMana < 0) currentMana = 0;
    }
}
