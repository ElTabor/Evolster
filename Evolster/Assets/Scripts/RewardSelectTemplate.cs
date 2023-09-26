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

    [SerializeField] TextMeshProUGUI name;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI description;

    public void ChooseReward()
    {
        Upgrade.instance.RewardSelection(rewardType);
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
