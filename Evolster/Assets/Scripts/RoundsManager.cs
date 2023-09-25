using System.Collections;
using System.Linq;
using UnityEngine;

public class RoundsManager : MonoBehaviour
{
    public static RoundsManager instance;

    public int enemiesOnScreen;
    public bool prepTime;
    public int round;
    private GameObject[] _gameObjects;

    void Start()
    {
        _gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        //prepTime = true;
        //StartCoroutine(PrepTimeSet());
    }

    void Update()
    {
        //if (prepTime) return;
        enemiesOnScreen = _gameObjects.Count();

        if (enemiesOnScreen <= 0) EndRound();
    }

    private IEnumerator PrepTimeSet()
    {
        yield return new WaitForSeconds(2.5f);
        prepTime = false;
    }

    private void EndRound()
    {
        //UIManager.instance.OpenCloseMenu(UIManager.instance.spellSelectionMenu);
        //prepTime = true;
    }

    public void SetNewRound()
    {
        //UIManager.instance.OpenCloseMenu(UIManager.instance.spellSelectionMenu);
        round++;
        if (round == 7) SceneManagerScript.instance.LoadNewScene("Lobby");
        EnemySpawnManager.instance.SetEnemiesToSpawn();
        StartCoroutine(PrepTimeSet());
    }
}
