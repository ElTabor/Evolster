using UnityEngine;

[CreateAssetMenu(fileName = "New Spell Upgrade Data", menuName = "Upgrade data")]

public class SpellsUpgradeData : ScriptableObject
{
    public int spellLevel;
    public int spellDamage;
    public int spellSpeed;
    public float spellLifeTime;
    public float spellDamageArea;
    public float spellDamageRange;
}
