using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Upgrade : MonoBehaviour
{
    public static Upgrade instance;
    private SpellsData[] spells;
    [SerializeField] private List<GameObject> spellsList;

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);


        //foreach (GameObject spell in PlayerController.instance.availableSpells)
        //    spells.Append(spell.GetComponent<SpellScript>().spellsData);
    }

    public void RewardSelection(string rewardType, string reward)
    {
        switch (rewardType)
        {
            case "StatUpgrade":
                switch(reward)
                {
                    case "Mejora de vida":
                        PlayerController.Instance.gameObject.GetComponent<LifeController>().IncreaseMaxLife(25);
                        break;
                    case "Vida maxima":
                        PlayerController.Instance.gameObject.GetComponent<LifeController>().UpdateLife(-PlayerController.Instance.gameObject.GetComponent<LifeController>().maxLife);
                        break;
                    case "Mejora de velocidad":
                        PlayerController.Instance.currentSpeed += 2;
                        break;
                }
                break;

            case "SpellUpgrade":
                foreach (SpellsData spelldata in spells)
                    spelldata.spellDamage += 10;
                break;

            case "NewSpell":
                int n = Random.Range(0, spellsList.Count());
                Debug.Log(n);
                PlayerController.Instance.spellController.UnlockNewSpell(spellsList[n]);
                break;
        }
    }
}
