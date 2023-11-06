using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Store Item Data", menuName = "Store Item data")]
public class StoreItemData : ScriptableObject
{
    public Sprite itemImage;
    public string itemName;
    public string itemDescription;
    public int itemValue;
    public GameObject spell;
}
