using UnityEngine;

[CreateAssetMenu ( fileName = "New StatData", menuName = "Stat Data")]
public class StatsData : ScriptableObject
{
    public int maxLife;
    public int maxMana;
    public float speed;
    public float mana;
    public int damage;
    public float speedAttack;
    public float attackRange;
    public float attackArea;

}
