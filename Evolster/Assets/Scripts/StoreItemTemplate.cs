using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemTemplate : MonoBehaviour
{
    public StoreItemData itemData;
    [SerializeField] Image _itemImage;
    [SerializeField] TextMeshProUGUI _itemName;
    [SerializeField] TextMeshProUGUI _itemDescription;
    [SerializeField] GameObject _buyButton;

    void Start() => SetValues();

    void SetValues()
    {
        _itemImage.sprite = itemData.itemImage;
        _itemName.text = itemData.itemName;
        _itemDescription.text = itemData.itemDescription;
        _buyButton.GetComponentInChildren<TextMeshProUGUI>().text = itemData.itemValue.ToString();
    }

    public void BuyItem()
    {
        if (PlayerController.instance.currencyController.currentCoins >= itemData.itemValue)
        {
            PlayerController.instance.currencyController.ManageCoins(-itemData.itemValue);
            PlayerController.instance.spellController.spells.Add(itemData.spell);
            _buyButton.GetComponentInChildren<Button>().interactable = false;
            _buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "You already own this item";
            UIManager.instance.itemsToDisplay.Remove(itemData);

        }
        else Debug.Log("Not enough coins");
    }
}
