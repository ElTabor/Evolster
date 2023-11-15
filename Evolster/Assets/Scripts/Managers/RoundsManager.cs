using System.Collections;
using System.Linq;
using UnityEngine;

public class RoundsManager : MonoBehaviour
{
    public static RoundsManager instance;

    public int enemiesOnScreen;
    public bool prepTime;
    public int round;

    [SerializeField] GameObject[] abilityPrefabs;

    private void Start()
    {
        instance = this;

        prepTime = true;
        SetNewRound();
    }

    private void Update()
    {
        if (prepTime) return;
        enemiesOnScreen = GameObject.FindGameObjectsWithTag("Enemy").Count();
        if (enemiesOnScreen <= 0) EndRound();
    }

    private IEnumerator PrepTimeSet()
    {
        yield return new WaitForSeconds(3f);
        prepTime = false;
    }

    private void EndRound()
    {
        if(SceneManager.instance.scene == "Level 1" || SceneManager.instance.scene == "Level 2")
        {
            if (round <= 5) UIManager.instance.OpenCloseMenu(UIManager.instance.rewardSelectionMenu);
            else if (round == 6)
            {
                UIManager.instance.OpenCloseMenu(UIManager.instance.abilityRewardPanel);
                GameObject ability;
                switch(SceneManager.instance.scene)
                {
                    case "Level 1":
                        ability = abilityPrefabs[0];
                        break;
                    case "Level 2":
                        ability = abilityPrefabs[1];
                        break;
                    case "Level 3":
                        ability = abilityPrefabs[2];
                        break;
                    default:
                        ability = abilityPrefabs[0];
                        Debug.Log("Error en seleccion de habilidad");
                        break;
                }
                UIManager.instance.SetAbilityRewardPanel(ability);
            }
        }
        prepTime = true;
    }

    public void SetNewRound()
    {
        round++;
        if (round == 7)
            GameManager.instance.EndLevel();
        StartCoroutine(PrepTimeSet());
        if (round < 7) EnemySpawnManager.instance.SetEnemiesToSpawn();
    }
}