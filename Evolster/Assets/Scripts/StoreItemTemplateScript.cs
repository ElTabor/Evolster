using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemTemplateScript : MonoBehaviour
{
    public StoreItemData itemData;
    [SerializeField] Image _itemImage;
    [SerializeField] TextMeshProUGUI _itemName;
    [SerializeField] TextMeshProUGUI _itemDescription;
    [SerializeField] GameObject _buyButton;

    void Start()
    {
        SetValues();
    }

    void SetValues()
    {
        _itemImage.sprite = itemData.itemImage;
        _itemName.text = itemData.itemName;
        _itemDescription.text = itemData.itemDescription;
        _buyButton.GetComponentInChildren<TextMeshProUGUI>().text = itemData.itemValue.ToString();
    }

    public void BuyItem()
    {
        if (PlayerController.instance.currency >= itemData.itemValue)
        {
            PlayerController.instance.currency -= itemData.itemValue;
            PlayerController.instance.spellController.spells.Add(itemData.spell);
            _buyButton.GetComponentInChildren<Button>().enabled = false;
        }
        else Debug.Log("Not enough currency");
    }
}
