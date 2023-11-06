using UnityEngine;

[CreateAssetMenuAttribute(fileName = "New Spell Data", menuName = "Spell data")]

public class SpellsData : ScriptableObject
{
    public Sprite spellSprite;
    public string spellName;
    public string spellType;
    public string spellDescription;
    public int spellLevel;
    public int spellDamage;
    public int spellSpeed;
    public float spellLifeTime;
    public float spellDamageRange;
    public float spellDamageArea;
}

