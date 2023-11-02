using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject spellSelectionMenu;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] public GameObject hud;

    [SerializeField] private GameObject lifeBar;
    [SerializeField] private Image lifeBarFill;
    [SerializeField] private GameObject manaBar;
    [SerializeField] private Image manaBarFill;
    public GameObject buffBar;
    [SerializeField] private Image buffBarFill;
    [SerializeField] private GameObject enemyCount;
    [SerializeField] private TextMeshProUGUI enemyCountText;
    [SerializeField] private Image[] hotkeysIcons;
    [SerializeField] private TextMeshProUGUI timerText;

    public float _timeElapsed;
    private int _minutes, _seconds;

    private bool _gamePaused;

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        _gamePaused = spellSelectionMenu.activeInHierarchy || pauseMenu.activeInHierarchy || gameOverMenu.activeInHierarchy;
        if (_gamePaused) Time.timeScale = 0f;
        else Time.timeScale = 1f;

        hud.SetActive(SceneManagerScript.instance.scene != "Main Menu" && SceneManagerScript.instance.scene != "Lobby");
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManagerScript.instance.scene != "Main Menu") OpenCloseMenu(pauseMenu);

        if (SceneManagerScript.instance.scene != "Main Menu" && SceneManagerScript.instance.scene != "Lobby")
        {
            UpdateLife();
            UpdateMana();
            UpdateTimer();
            StopTimer();
            enemyCount.SetActive(true);
            enemyCountText.text = "x " + GameObject.FindGameObjectsWithTag("Enemy").Count().ToString();
            // for(int n = 0; n <= hotkeysIcons.Length; n++)
            // {
            //     if (hotkeysIcons[n-1] != null)
            //     hotkeysIcons[n-1].sprite = PlayerController.instance.spellController.spells[n-1].GetComponent<SpellScript>().spellData.spellSprite;
            //     else hotkeysIcons[n-1].sprite = null;
            // }
        }
        else
        {
           if(SceneManagerScript.instance.scene == "Main Menu") mainMenu.SetActive(true);
           else mainMenu.SetActive(false);
           lifeBar.SetActive(false);
           enemyCount.SetActive(false);
        }
    }

    private void UpdateLife()
    {
        lifeBar.SetActive(true);
        if (SceneManagerScript.instance.scene != "Main Menu" && SceneManagerScript.instance.scene != "Lobby")
            lifeBarFill.fillAmount =  PlayerController.instance.GetComponent<LifeController>().currentLife / PlayerController.instance.GetComponent<LifeController>().maxLife;
    }

    private void UpdateMana()
    {
        manaBar.SetActive(true);
        if (SceneManagerScript.instance.scene != "Main Menu" && SceneManagerScript.instance.scene != "Lobby")
            manaBarFill.fillAmount = PlayerController.instance.manaController.currentMana / PlayerController.instance.stats.maxMana;
    }

    public void UpdateBuff(float applyTime, float buffTime)
    {
        float fillAmount = (Time.time - applyTime) / buffTime;
        buffBarFill.fillAmount = fillAmount;
    }
    private void UpdateTimer()
    {
        _timeElapsed += Time.deltaTime;
        _minutes = (int)(_timeElapsed / 60f);
        _seconds = (int)(_timeElapsed - _minutes * 60f);

        timerText.text = string.Format("{0:00}:{1:00}", _minutes, _seconds);
    }

    public void OpenCloseMenu(GameObject menu) => menu.SetActive(!menu.activeInHierarchy);

    public void GameOver()
    {
        OpenCloseMenu(gameOverMenu);
        StopTimer();
    }

    private void StopTimer() => PlayerPrefs.SetInt("HighScore", (int)_timeElapsed);

    public void ChangeScene(string newScene) => SceneManagerScript.instance.LoadNewScene(newScene);

    public void ExitGame() => SceneManagerScript.instance.Exit();
}