using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;

    public int currentLevel;

    public bool gamePaused;

    void Start()
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
    }

    void Restart()
    {
        PlayerController.instance.GetComponent<LifeController>().SetMaxLife(PlayerController.instance.playerStats.maxLife);
        PlayerController.instance.GetComponent<ManaController>().SetMaxMana(PlayerController.instance.playerStats.maxMana);
        UIManager.instance._timeElapsed = 0;
    }
}
