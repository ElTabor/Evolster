using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAbility
{
    UniqueAbilityData abilityData { get; set; }

    float currentDamage { get; set; }
    LayerMask enemiesLayer { get; set; }

    void Start();
    void Update();
}
