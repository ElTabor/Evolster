using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "New Ability Data", menuName = "Ability data")]
public class UniqueAbilityData : ScriptableObject
{
    public Sprite uniqueAbilitySprite;
    public string uniqueAbilityName;
    public string uniqueAbilityType;
    public string uniqueAbilityDescription;
    public int uniqueAbilityLevel;
    public int uniqueAbilityDamage;
    public int uniqueAbilitySpeed;
    public float uniqueAbilityLifeTime;
    public float uniqueAbilityDamageRange;
    public float uniqueAbilityDamageArea;
}
