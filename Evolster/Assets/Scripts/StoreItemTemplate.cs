using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
    }

    public void BuyItem()
    {
        if (PlayerController.instance.currencyController.currentCoins >= itemData.itemValue)
        {
            PlayerController.instance.currencyController.ManageCoins(-itemData.itemValue);
            _buyButton.GetComponentInChildren<Button>().interactable = false;

            for (int i = 0; i < PlayerController.instance.spellController.spells.Count; i++)
            {
                if (PlayerController.instance.spellController.spells[i] == null)
                {
                    PlayerController.instance.spellController.spells[i] = itemData.spell;
                   i = PlayerController.instance.spellController.spells.Count;
                }
            }
            
            UIManager.instance.itemsToDisplay.Remove(itemData);

        }
        else Debug.Log("Not enough coins");
    }
}
