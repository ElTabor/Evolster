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

    //private List<ScoreInfo> GetScoresInOrder()
    //{
    //    List<ScoreInfo> scoresInOrder = new List<ScoreInfo>();
    //    Stack<float> scoreRecord = highscoresManager.GetScoreRecord();

    //    foreach(int score in scoreRecord)
    //    {
    //        ScoreInfo scoreInfo = new ScoreInfo();
    //        scoreInfo.playerName = "Player Name";
    //        scoreInfo.playerScore = score;

    //        scoresInOrder.Add(scoreInfo);
    //    }

    //    scoresInOrder.Sort((a, b) => b.playerScore.CompareTo(a.playerScore));
    //    return scoresInOrder;
    //}
}
