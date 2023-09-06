using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro.EditorUtilities;
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

        prepTime = true;
        StartCoroutine(prepTimeSet());
    }

    void Update()
    {

        if(!prepTime)
        {
            enemiesOnScreen = GameObject.FindGameObjectsWithTag("Enemy").Count();

            if (enemiesOnScreen <= 0) EndRound();
        }
    }

    IEnumerator prepTimeSet()
    {
        yield return new WaitForSeconds(2f);
        prepTime = false;
    }

    void EndRound()
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
        StartCoroutine(prepTimeSet());
    }
}