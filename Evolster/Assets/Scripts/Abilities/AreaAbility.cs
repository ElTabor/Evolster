using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class AreaAbility : MonoBehaviour, IAbility
{
    [field: SerializeField] public UniqueAbilityData abilityData { get; set; }

    public float currentDamage { get; set; }

    [field: SerializeField] public LayerMask enemiesLayer { get; set; }

    [SerializeField] GameObject abilityPrefab;

    public void Start()
    {
        currentDamage = abilityData.uniqueAbilityDamageArea;
    }
    public void Update()
    {
        if (Input.GetMouseButtonDown(0)) TryCastAbility();
    }

    void TryCastAbility()
    {
        if (PlayerController.instance.abilityController.abilityAvailable && !GameManager.instance.gamePaused && GameManager.instance.onLevel) CastAbility();
    }

    void CastAbility()
    {
        GameObject uniqueAbility = Instantiate(abilityPrefab, PlayerController.instance.transform.position, Quaternion.identity);
        var aimDirection = (PlayerController.instance.aim.position - PlayerController.instance.transform.position).normalized;
        uniqueAbility.GetComponent<AreaAbilityBullet>().damageArea += currentDamage;
        uniqueAbility.GetComponent<AreaAbilityBullet>().currentDamage += abilityData.uniqueAbilityDamage;
        uniqueAbility.GetComponent<AreaAbilityBullet>().direction = aimDirection;
        uniqueAbility.GetComponent<AreaAbilityBullet>().speed = abilityData.uniqueAbilitySpeed;
        uniqueAbility.GetComponent<AreaAbilityBullet>().lifeTime += abilityData.uniqueAbilityLifeTime;
        PlayerController.instance.manaController.ManageMana(-abilityData.manaCost);
        Debug.Log("Ability Cast");
    }
}