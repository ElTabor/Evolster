using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardSelectTemplate : MonoBehaviour
{
    public GameObject spellSelectionMenu;
    public string rewardName;
    public Sprite rewardIcon;
    public string rewardDescription;
    public string rewardType;

    [SerializeField] private TextMeshProUGUI name;
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI description;

    public void ChooseReward()
    {
        Upgrade.instance.RewardSelection(rewardType, rewardName);
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
