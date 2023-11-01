using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerText;

    private float _timeElapsed;
    private int _minutes, _seconds;
    private bool _startTimer;
    private LifeController _lifeController;

    //public TextMeshProUGUI highscore;

    private void Start()
    {
        _startTimer = true;
        //highscore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }

    private void Update()
    {
        UpdateTimer();
        StopTimer();
    }

    private void UpdateTimer()
    {
        _timeElapsed += Time.deltaTime;
        _minutes = (int)(_timeElapsed / 60f);
        _seconds = (int)(_timeElapsed - _minutes * 60f);

        timerText.text = string.Format("{0:00}:{1:00}", _minutes, _seconds);
    }

    private void StopTimer()
    {
        if (PlayerController.instance.isDead)
        {
            Debug.Log(_timeElapsed);
            Time.timeScale = 0f;
            Debug.Log("Game over");
            
            PlayerPrefs.SetInt("HighScore", (int)_timeElapsed);
            Debug.Log(PlayerPrefs.GetInt("HighScore"));
        }
    }
}
