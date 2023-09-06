using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute (fileName = "New Spell Data", menuName = "Spell data")]

public class SpellsData : ScriptableObject
{
    [SerializeField] private string spellName;
    [SerializeField] private string spellType;
    [SerializeField] private string description;
    [SerializeField] private int damage;
    [SerializeField] private int speed;
    [SerializeField] private int level;
    [SerializeField] private float lifeTime;


    public string SpellName { get { return spellName; } }
    public string SpellType { get { return spellType; } }
    public string Description { get { return description; } }
    public int Damage { get { return damage; } }
    public int Level { get { return level; } }
    public int Speed { get { return speed; } }
    public float LifeTime { get { return lifeTime; } }
}
