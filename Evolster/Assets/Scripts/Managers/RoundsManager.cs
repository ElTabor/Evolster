using System.Collections;
using System.Linq;
using UnityEngine;

public class RoundsManager : MonoBehaviour
{
    public static RoundsManager instance;

    public int enemiesOnScreen;
    public bool prepTime;
    public int round;

    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        //if (SceneManagerScript.instance.scene == "Lobby") gameObject.SetActive(false);

        prepTime = true;
        SetNewRound();
    }

    void Update()
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
        if(SceneManager.instance.scene == "Level 1" || SceneManager.instance.scene == "Endless") UIManager.instance.OpenCloseMenu(UIManager.instance.spellSelectionMenu);
        prepTime = true;
    }

    public void SetNewRound()
    {
        StartCoroutine(PrepTimeSet());
        if (round == 7) GameManager.instance.ChangeScene("Lobby");
        else if (round > 0) UIManager.instance.OpenCloseMenu(UIManager.instance.spellSelectionMenu);
        round++;
        EnemySpawnManager.instance.SetEnemiesToSpawn();
    }
}
