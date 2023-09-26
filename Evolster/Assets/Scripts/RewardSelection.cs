using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RewardSelection : MonoBehaviour
{
    [SerializeField] GameObject rewardPrefab;
    [SerializeField] RewardData[] rewards;
    [SerializeField] GameObject spellSelectorPanel;

    int n = 0;

    void Start()
    {
        for(int n = 0; n < rewards.Length; n++) InstantiateChoice(n);
    }

    GameObject InstantiateChoice(int i)
    {
        GameObject newChoice = Instantiate(rewardPrefab, spellSelectorPanel.transform);

        newChoice.GetComponent<RewardSelectTemplate>().rewardDescription = rewards[i].description;
        newChoice.GetComponent<RewardSelectTemplate>().rewardIcon = rewards[i].sprite;
        newChoice.GetComponent<RewardSelectTemplate>().spellSelectionMenu = gameObject;
        newChoice.GetComponent<RewardSelectTemplate>().rewardType = rewards[i].type;
        newChoice.GetComponent<RewardSelectTemplate>().rewardName = rewards[i].name;
        newChoice.GetComponent<RewardSelectTemplate>().SetValues();

        return newChoice;
    }
}
