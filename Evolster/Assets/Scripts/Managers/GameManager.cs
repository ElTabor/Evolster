using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;

    public int currentLevel;

    public bool gamePaused;


    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ChangeScene(string newScene)
    {
        PlayerController.instance.transform.position = Vector3.zero;
        SceneManager.instance.LoadNewScene(newScene);
        switch (newScene)
        {
            case "Main Menu":
                Restart();
                break;
            case "Lobby":
                Debug.Log("SetDoors");
                break;
            default:
                Debug.Log("Error en LoadNewScene");
                break;
        }

        if (SceneManager.instance.scene != "Lobby" || SceneManager.instance.scene != "Main Menu")
        {
            if(RoundsManager.instance != null)
                RoundsManager.instance.round = 0;
        }
    }

    private void Restart()
    {
        PlayerController.instance.GetComponent<LifeController>().SetMaxLife(PlayerController.instance.playerStats.maxLife);
        PlayerController.instance.GetComponent<ManaController>().SetMaxMana(PlayerController.instance.playerStats.maxMana);
        UIManager.instance._timeElapsed = 0;
    }

    public void EndLevel()
    {
        if (RoundsManager.instance.round == 6) ChangeScene("Lobby");
        currentLevel++;
    }
}
