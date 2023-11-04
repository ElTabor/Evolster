using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Highscores : MonoBehaviour
{
    public TextMeshProUGUI highscoreText;
    private void Start()
    {
        highscoreText.text = PlayerPrefs.GetFloat("Highscore").ToString();
    }

    private void Update()
    {
        SetScore();
    }

    private void SetScore()
    {
        float timeElapsed = PlayerPrefs.GetFloat("Highscore");
        float _minutes = (int)(timeElapsed / 60f);
        float _seconds = (int)(timeElapsed - _minutes * 60f);

        highscoreText.text = string.Format("{0:00}:{1:00}", _minutes, _seconds);
    }
}