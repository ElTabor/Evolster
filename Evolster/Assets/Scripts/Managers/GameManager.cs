using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject player;

    public int currentLevel;

    public bool gamePaused;
    public bool onLevel;

    public List<TextMeshProUGUI> scoreTextListPrefab = new List<TextMeshProUGUI>();

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (gamePaused) Time.timeScale = 0f;
        else Time.timeScale = 1f;
        UpdateHighScores();
    }

    public void UpdateHighScores()
    {
        for (int i = 0; i < HighScoreController.instance.scoreTextList.Count && i < 5; i++)
        {
            if (UIManager.instance.highScorePanel.activeInHierarchy)
            {
                Debug.Log("F");
                scoreTextListPrefab[i].gameObject.SetActive(scoreTextListPrefab[i] != null);
                float timeElapsed = HighScoreController.instance.scoreTextList[i];
                float _minutes = (int)(timeElapsed / 60f);
                float _seconds = (int)(timeElapsed - _minutes * 60f);
                
                scoreTextListPrefab[i].text = string.Format("{0:00}:{1:00}", _minutes, _seconds);
                Debug.Log(scoreTextListPrefab[i].text);
            }
        }
        HighScoreController.instance.GetSortedScores();
    }

    public void ChangeScene(string newScene)
    {
        PlayerController.instance.transform.position = Vector3.zero;
        SceneManager.instance.LoadNewScene(newScene);
        switch (newScene)
        {
            case "Main Menu":
                Restart();
                AudioController.instance.PlayMusic(AudioController.instance.tracks[0]);
                break;
            case "Lobby":
                Debug.Log("SetDoors");
                AudioController.instance.PlayMusic(AudioController.instance.tracks[1]);
                break;
            case "Cementery":
                AudioController.instance.PlayMusic(AudioController.instance.tracks[2]);
                break;
            case "DarkForest":
                AudioController.instance.PlayMusic(AudioController.instance.tracks[3]);
                break;
            case "Castle":
                AudioController.instance.PlayMusic(AudioController.instance.tracks[4]);
                break;
            case "Endless":
                AudioController.instance.PlayMusic(AudioController.instance.tracks[5]);
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
        ChangeScene("Lobby");
        currentLevel++;
    }
}
