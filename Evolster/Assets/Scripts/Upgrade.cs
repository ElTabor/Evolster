using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Upgrade : MonoBehaviour
{
    public static Upgrade instance;
    private StatsData player;
    private SpellsData[] spells;
    [SerializeField] GameObject[] spellsList;

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        player = PlayerController.instance._stats;


        foreach (GameObject spell in PlayerController.instance.availableSpells)
            spells.Append(spell.GetComponent<SpellScript>().spellsData);
    }

    public void RewardSelection(string rewardSelected)
    {
        switch (rewardSelected)
        {
            case "StatUpgrade":
                player.speed += 5;
                break;

            case "SpellUpgrade":
                foreach (SpellsData spelldata in spells)
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
