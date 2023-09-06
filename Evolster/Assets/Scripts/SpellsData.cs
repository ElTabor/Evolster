using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute (fileName = "New Spell Data", menuName = "Spell data")]

public class SpellsData : ScriptableObject
{
    [SerializeField] private string spellName;
    [SerializeField] private string spellType;
    [SerializeField] private string spellDescription;
    [SerializeField] private int spellDamage;
    [SerializeField] private int spellSpeed;
    [SerializeField] private int spellLevel;
    [SerializeField] private float spellLifeTime;


    public string Name { get { return spellName; } }
    public string Type { get { return spellType; } }
    public string Description { get { return spellDescription; } }
    public int Damage { get { return spellDamage; } }
    public int Level { get { return spellLevel; } }
    public int Speed { get { return spellSpeed; } }
    public float LifeTime { get { return spellLifeTime; } }
}
