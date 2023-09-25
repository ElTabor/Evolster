using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade Data", menuName = "Upgrade data")]
public class RewardData : ScriptableObject
{
    public string name;
    public Sprite sprite;
    public string description;
}
