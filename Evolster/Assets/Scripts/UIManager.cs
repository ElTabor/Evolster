using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject spellSelectionMenu;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject HUD;

    [SerializeField] GameObject lifeBar;
    [SerializeField] Image lifeBarFill;
    [SerializeField] GameObject manaBar;
    [SerializeField] Image manaBarFill;
    public GameObject buffBar;
    [SerializeField] Image BuffBarFill;
    [SerializeField] GameObject enemyCount;
    [SerializeField] TextMeshProUGUI enemyCountText;

    bool gamePaused;

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

    }
    private void Update()
    {
        gamePaused = spellSelectionMenu.activeInHierarchy || pauseMenu.activeInHierarchy || gameOverMenu.activeInHierarchy;
        if (gamePaused) Time.timeScale = 0f;
        else Time.timeScale = 1f;

        HUD.SetActive(SceneManagerScript.instance.scene != "Main Menu" && SceneManagerScript.instance.scene != "Lobby");
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManagerScript.instance.scene != "Main Menu") OpenCloseMenu(pauseMenu);

        if (SceneManagerScript.instance.scene != "Main Menu" && SceneManagerScript.instance.scene != "Lobby")
        {
            UpdateLife();
            UpdateMana();
            enemyCount.SetActive(true);
            enemyCountText.text = "x " + GameObject.FindGameObjectsWithTag("Enemy").Count().ToString();
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
            lifeBarFill.fillAmount = PlayerController.instance.gameObject.GetComponent<LifeController>()._currentLife / PlayerController.instance.gameObject.GetComponent<LifeController>()._maxLife;
    }

    private void UpdateMana()
    {
        manaBar.SetActive(true);
        if (SceneManagerScript.instance.scene != "Main Menu" && SceneManagerScript.instance.scene != "Lobby")
            manaBarFill.fillAmount = PlayerController.instance.manaController.currentMana / PlayerController.instance._stats.maxMana;
    }

    public void UpdateBuff(float applyTime, float buffTime)
    {
        float fillAmount = (Time.time - applyTime) / buffTime;
        BuffBarFill.fillAmount = fillAmount;
    }

    public void OpenCloseMenu(GameObject menu) => menu.SetActive(!menu.activeInHierarchy);

    public void GameOver() => OpenCloseMenu(gameOverMenu);

    public void ChangeScene(string newScene) => SceneManagerScript.instance.LoadNewScene(newScene);

    public void ExitGame() => SceneManagerScript.instance.Exit();
}