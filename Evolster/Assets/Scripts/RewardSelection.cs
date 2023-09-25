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

    void Start()
    {
        for(int n = 0; n < rewards.Count(); n++) InstantiateChoice();
    }

    void InstantiateChoice()
    {
        Instantiate(rewardPrefab, spellSelectorPanel.transform);
        rewardPrefab.AddComponent<RewardSelectTemplate>();
        rewardPrefab.GetComponent<RewardSelectTemplate>().rewardName = rewards[0].name;
        rewardPrefab.GetComponent<RewardSelectTemplate>().rewardDescription = rewards[0].description;
        rewardPrefab.GetComponent<RewardSelectTemplate>().rewardIcon = rewards[0].sprite;
        rewardPrefab.GetComponent<RewardSelectTemplate>().spellSelectionMenu = gameObject;
        rewardPrefab.GetComponent<RewardSelectTemplate>().SetValues();
    }
}
