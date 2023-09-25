using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using Random = UnityEngine.Random;

public class Upgrade : MonoBehaviour
{
    public static Upgrade instance;
    private StatsData player;
    private SpellsData[] spell;
    [SerializeField] GameObject[] spellsList;

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        player = PlayerController.instance._stats;
        
        foreach (GameObject spell in PlayerController.instance.availableSpells)
        {
            this.spell.Append(spell.GetComponent<SpellsData>());
        }
    }

    public void RewardSelection(string rewardSelected)
    {
        switch (rewardSelected)
        {
            case "StatUpgrade":
                player.speed += 5;
                break;
            case "SpellUpgrade":
                foreach (SpellsData spelldata in spell)
                {
                    spelldata.spellDamage += 10;
                }
                break;
            case "NewSpell":
                int n = Random.Range(0, spellsList.Length);
                Debug.Log(n);
                PlayerController.instance.availableSpells.Append(spellsList[n]);
                Debug.Log(PlayerController.instance.availableSpells);
                break;
        }
    }
}
