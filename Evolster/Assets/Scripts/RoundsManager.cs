using System.Collections;
using System.Linq;
using UnityEngine;

public class RoundsManager : MonoBehaviour
{
    public static RoundsManager Instance;

    public int enemiesOnScreen;
    public bool prepTime;
    public int round;
    private GameObject[] _gameObjects;

    void Start()
    {
        _gameObjects = GameObject.FindGameObjectsWithTag("Enemy");
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        prepTime = true;
        StartCoroutine(PrepTimeSet());
    }

    void Update()
    {
        if (prepTime) return;
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
        UIManager.instance.ToggleUICanvas(UIManager.instance.skillCanvas);
        prepTime = true;
    }

    public void SetNewRound()
    {
        UIManager.instance.ToggleUICanvas(UIManager.instance.skillCanvas);
        round++;
        if (round == 7) SceneManagerScript.instance.LoadNewScene("Lobby");
        EnemySpawnManager.instance.SetEnemiesToSpawn();
        StartCoroutine(PrepTimeSet());
    }
}
