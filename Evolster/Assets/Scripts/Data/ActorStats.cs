using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu ( fileName = "New StatData", menuName = "Stat Data")]
public class ActorStats : ScriptableObject
{
    public int maxLife;
    public int maxMana;
    public float movementSpeed;
    public int damage;
    public float attackSpeed;
    public float attackRange;
    public float attackArea;

}
