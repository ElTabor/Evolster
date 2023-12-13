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
        yield return new WaitForSeconds(2f);
        if (round < 7) EnemySpawnManager.instance.SetEnemiesToSpawn();
        yield return new WaitForSeconds(1f);
        prepTime = false;
    }

    private void EndRound()
    {
        if(SceneManager.instance.scene == "Cementery" || SceneManager.instance.scene == "DarkForest" || SceneManager.instance.scene == "Castle")
        {
            if (round <= 5) UIManager.instance.OpenCloseMenu(UIManager.instance.rewardSelectionMenu);
            else if (round == 6)
            {
                UIManager.instance.OpenCloseMenu(UIManager.instance.abilityRewardPanel);
                GameObject ability;
                switch(SceneManager.instance.scene)
                {
                    case "Cementery":
                        ability = abilityPrefabs[0];
                        break;
                    case "DarkForest":
                        ability = abilityPrefabs[1];
                        break;
                    case "Castle":
                        ability = abilityPrefabs[2];
                        break;
                    default:
                        ability = abilityPrefabs[0];
                        Debug.Log("Error en seleccion de habilidad");
                        break;
                }
                if(SceneManager.instance.scene != "Lobby" || SceneManager.instance.scene != "MainMenu") UIManager.instance.SetAbilityRewardPanel(ability);
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
    }
}