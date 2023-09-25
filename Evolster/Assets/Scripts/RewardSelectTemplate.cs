using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardSelectTemplate : MonoBehaviour
{
    public GameObject spellSelectionMenu;
    public string rewardName;
    public Sprite rewardIcon;
    public string rewardDescription;

    [SerializeField] TextMeshProUGUI name;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI description;

    public void ChooseReward()
    {
        PlayerController.instance.GetReward(rewardName);
        CloseMenu();
    }

    void CloseMenu()
    {
        RoundsManager.instance.SetNewRound();
    }

    public void SetValues()
    {
        name.text = rewardName;
        icon.sprite = rewardIcon;
        description.text = rewardDescription;
    }
}
