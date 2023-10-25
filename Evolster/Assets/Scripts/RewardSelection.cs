using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RewardSelection : MonoBehaviour
{
    [SerializeField] GameObject rewardPrefab;
    [SerializeField] RewardData[] statUpgrades;
    [SerializeField] RewardData[] spellUpgrades;
    [SerializeField] RewardData[] newSpells;
    [SerializeField] GameObject spellSelectorPanel;

    void Start()
    {
        for(int n = 0; n < 3; n++)
        {
            int r;
            switch (n)
            {
                case 0:
                    r = Random.Range(0, statUpgrades.Length);
                    InstantiateChoice(statUpgrades, r);
                    break;
                case 1:
                    r = Random.Range(0, spellUpgrades.Length);
                    InstantiateChoice(spellUpgrades, r);
                    break;
                case 2:
                    r = Random.Range(0, newSpells.Length);
                    InstantiateChoice(newSpells, r);
                    break;
            }
        }
    }

    GameObject InstantiateChoice(RewardData[] rewards, int a)
    {
        GameObject newChoice = Instantiate(rewardPrefab, spellSelectorPanel.transform);

        newChoice.GetComponent<RewardSelectTemplate>().rewardDescription = rewards[a].description;
        newChoice.GetComponent<RewardSelectTemplate>().rewardIcon = rewards[a].sprite;
        newChoice.GetComponent<RewardSelectTemplate>().spellSelectionMenu = gameObject;
        newChoice.GetComponent<RewardSelectTemplate>().rewardType = rewards[a].type;
        newChoice.GetComponent<RewardSelectTemplate>().rewardName = rewards[a].name;
        newChoice.GetComponent<RewardSelectTemplate>().SetValues();

        return newChoice;
    }
}
