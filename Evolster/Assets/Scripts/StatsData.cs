using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ( fileName = "New StatData", menuName = "Stat Data")]
public class StatsData : ScriptableObject
{
    public int maxLife;
    public float speed;
    public int damage;
    public float fireRate;
    public float attackCooldown;
    public float rangeOfAttack;

}
