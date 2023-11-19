using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoresManager : MonoBehaviour
{
    private Stack<float> highscoresRecord = new Stack<float>();
    private float currentScore;
    public static HighScoresManager instance;

    private void Awake()
    {
        instance = this;
    }
    

    public float GetScore()
    {
        currentScore = UIManager.instance._timeElapsed;
        return currentScore;
    }

    public void AddScore(float scoreToAdd) => currentScore = scoreToAdd;

    public Stack<float> GetScoreRecord()
    {
        return highscoresRecord;
    }

    public void SaveHighscore()
    {
        highscoresRecord.Push(currentScore);
    }

    public void ResetHighscore() => currentScore = 0;
}
