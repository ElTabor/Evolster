using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject rewardSelectionMenu;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    [SerializeField] private GameObject mainMenu;
    [SerializeField] public GameObject hud;
    [SerializeField] public GameObject store;


    [Header ("HUD")]
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

    [Header("STORE")]
    [SerializeField] StoreItemData[] itemsToDisplay;
    [SerializeField] GameObject storeItemTemplate;

    [Header ("HIGHSCORES")]
    public float _timeElapsed;
    private int _minutes, _seconds;


    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        GameManager.instance.gamePaused = pauseMenu.activeInHierarchy || gameOverMenu.activeInHierarchy || store.activeInHierarchy;
        if (GameManager.instance.gamePaused) Time.timeScale = 0f;
        else Time.timeScale = 1f;

        hud.SetActive(SceneManager.instance.scene != "Main Menu" && SceneManager.instance.scene != "Lobby");
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.instance.scene != "Main Menu") OpenCloseMenu(pauseMenu);

        if (SceneManager.instance.scene != "Main Menu" && SceneManager.instance.scene != "Lobby")
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
           if(SceneManager.instance.scene == "Main Menu") mainMenu.SetActive(true);
           else mainMenu.SetActive(false);
           lifeBar.SetActive(false);
           enemyCount.SetActive(false);
        }
    }

    private void UpdateLife()
    {
        lifeBar.SetActive(true);
        if (SceneManager.instance.scene != "Main Menu" && SceneManager.instance.scene != "Lobby")
            lifeBarFill.fillAmount =  PlayerController.instance.GetComponent<LifeController>().currentLife / PlayerController.instance.GetComponent<LifeController>().maxLife;
    }

    private void UpdateMana()
    {
        manaBar.SetActive(true);
        if (SceneManager.instance.scene != "Main Menu" && SceneManager.instance.scene != "Lobby")
            manaBarFill.fillAmount = PlayerController.instance.manaController.currentMana / PlayerController.instance.playerStats.maxMana;
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

    public void SetStoreCanvas()
    {
        if(store.activeInHierarchy)
        {
            foreach (var item in store.GetComponentsInChildren<StoreItemTemplate>()) Destroy(item.gameObject);
            OpenCloseMenu(store);
        }
        else
        {
            OpenCloseMenu(store);
            foreach (var item in itemsToDisplay)
            {
                GameObject newItem = Instantiate(storeItemTemplate, store.GetComponentInChildren<GridLayoutGroup>().transform);
                newItem.GetComponent<StoreItemTemplate>().itemData = item;
            }
        }
    }

    private void StopTimer()
    {
        PlayerPrefs.SetFloat("Highscore", _timeElapsed);
        //Debug.Log(_timeElapsed);
    }

    public void ChangeScene(string newScene) => GameManager.instance.ChangeScene(newScene);

    public void ExitGame() => SceneManager.instance.Exit();
}