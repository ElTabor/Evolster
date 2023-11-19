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
    public GameObject abilityRewardPanel;
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
    [SerializeField] private TextMeshProUGUI coinsFeedback;
    private bool showingCoinsFeedback;
    public float lastFeedbackTime;
    [SerializeField] float feedbackTime;
    public int coinsToShow;

    [Header("STORE")]
    [SerializeField] StoreItemData[] itemsToDisplay;
    [SerializeField] GameObject storeItemTemplate;
    [SerializeField] TextMeshProUGUI coins;

    [Header ("HIGHSCORES")]
    public float _timeElapsed;
    private int _minutes, _seconds;

    [Header("ABILITY REWARD")]
    private GameObject levelAbility;
    [SerializeField] TextMeshProUGUI abilityName;
    [SerializeField] Image abilityIcon;
    [SerializeField] GameObject statsPanel;
    [SerializeField] TextMeshProUGUI[] stats;


    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        GameManager.instance.gamePaused = pauseMenu.activeInHierarchy || gameOverMenu.activeInHierarchy || store.activeInHierarchy || abilityRewardPanel.activeInHierarchy || rewardSelectionMenu.activeInHierarchy;

        hud.SetActive(SceneManager.instance.scene != "Main Menu" && SceneManager.instance.scene != "Lobby");
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.instance.scene != "Main Menu") OpenCloseMenu(pauseMenu);

        if (SceneManager.instance.scene != "Main Menu" && SceneManager.instance.scene != "Lobby")
        {
            UpdateLife();
            UpdateMana();
            UpdateTimer();
            UpdateCoinsFeedback();
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
        if (store.activeInHierarchy) UpdateCoins();
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

    private void UpdateCoins()
    {
        coins.text = $" x{PlayerController.instance.currencyController.currentCoins}";
    }

    private void UpdateCoinsFeedback()
    {
        if (Time.time > lastFeedbackTime + feedbackTime)
        {
            coinsToShow = 0;
            showingCoinsFeedback = false;
        }
        else showingCoinsFeedback = true;
        coinsFeedback.gameObject.SetActive(showingCoinsFeedback);
        coinsFeedback.text = $"+{coinsToShow}";
    }

    public void OpenCloseMenu(GameObject menu) => menu.SetActive(!menu.activeInHierarchy);

    public void GameOver()
    {
        OpenCloseMenu(gameOverMenu);
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

    public void SetAbilityRewardPanel(GameObject abilityInfo)
    {
        levelAbility = abilityInfo;
        abilityName.text = abilityInfo.GetComponent<AreaAbility>().uniqueAbilityData.uniqueAbilityName;
        abilityIcon.sprite = abilityInfo.GetComponent<AreaAbility>().uniqueAbilityData.uniqueAbilitySprite;
        stats[0].text = $"Level: {abilityInfo.GetComponent<AreaAbility>().uniqueAbilityData.uniqueAbilityLevel}";
        stats[1].text = $"Damage: {abilityInfo.GetComponent<AreaAbility>().uniqueAbilityData.uniqueAbilityDamage}";
        stats[2].text = $"Speed: {abilityInfo.GetComponent<AreaAbility>().uniqueAbilityData.uniqueAbilitySpeed}";
        stats[3].text = $"Duration: {abilityInfo.GetComponent<AreaAbility>().uniqueAbilityData.uniqueAbilityLifeTime}s";
        stats[4].text = $"Damage range: {abilityInfo.GetComponent<AreaAbility>().uniqueAbilityData.uniqueAbilityDamageRange}";
        stats[5].text = $"Damage area: {abilityInfo.GetComponent<AreaAbility>().uniqueAbilityData.uniqueAbilityDamageArea}";
        stats[6].text = $"Mana cost: {abilityInfo.GetComponent<AreaAbility>().uniqueAbilityData.manaCost}";
    }

    public void UnlockAbility()
    {
        OpenCloseMenu(abilityRewardPanel);
        PlayerController.instance.EquipAbility(levelAbility);
        RoundsManager.instance.SetNewRound();
    }

    public void ChangeScene(string newScene) => GameManager.instance.ChangeScene(newScene);

    public void ExitGame() => SceneManager.instance.Exit();
}