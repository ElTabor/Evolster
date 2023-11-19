using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ABBHighscores : MonoBehaviour
{
    public TextMeshProUGUI highScore;

    private ABB abb;
    private HighScoresManager highscoresManager;
    private string playerName;

    public class ScoreInfo
    {
        public string playerName;
        public int playerScore;
    }

    private void Start()
    {
        abb = new ABB();
        abb.InicializarArbol();
        highscoresManager = HighScoresManager.instance;
        playerName = "PlayerName";

        Stack<float> scoreRecord = highscoresManager.GetScoreRecord();
        foreach (int score in scoreRecord)
        {
            Player player = new Player(playerName, score);
            abb.AgregarElem(player);
        }

        List<Player> scoresInOrder = abb.GetPlayersInOrder();
        string highscoresText = "";

        for(int i = 0; i < scoresInOrder.Count; i++)
        {
            highscoresText += "Position " + (i + 1) + scoresInOrder[i].Name + " - Puntaje " + scoresInOrder[i].Score.ToString();
            
        }
        highScore.text = highscoresText;
    }

    private void Update()
    {
        SetScore();
    }

    private void SetScore()
    {
        float timeElapsed = UIManager.instance._timeElapsed;
        float _minutes = (int)(timeElapsed / 60f);
        float _seconds = (int)(timeElapsed - _minutes * 60f);

        highScore.text = string.Format("{0:00}:{1:00}", _minutes, _seconds);
    }
}
