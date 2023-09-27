using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject spellSelectionMenu;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    [SerializeField] GameObject mainMenu;

    //[SerializeField] Slider lifeBar;

    bool gamePaused;

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        //if(SceneManagerScript.instance.scene != "Main Menu" && SceneManagerScript.instance.scene != "Lobby")
        //    lifeBar = GameObject.FindWithTag("Player").GetComponentInChildren<Slider>();

    }
    private void Update()
    {
        gamePaused = spellSelectionMenu.activeInHierarchy || pauseMenu.activeInHierarchy || gameOverMenu.activeInHierarchy;
        if (gamePaused) Time.timeScale = 0f;
        else Time.timeScale = 1f;

        if (Input.GetKeyDown(KeyCode.Escape) && SceneManagerScript.instance.scene != "Main Menu") OpenCloseMenu(pauseMenu);

        //if (SceneManagerScript.instance.scene != "Main Menu" && SceneManagerScript.instance.scene != "Lobby") //UpdateLife();
        //else
        //{
           if(SceneManagerScript.instance.scene == "Main Menu") mainMenu.SetActive(true);
           else mainMenu.SetActive(false);
           //lifeBar?.gameObject.SetActive(false);
        //}
    }

    //private void UpdateLife()
    //{
    //  lifeBar?.gameObject.SetActive(true);
    //  if(SceneManagerScript.instance.scene != "Main Menu" && SceneManagerScript.instance.scene != "Lobby") 
    //      lifeBar.value = PlayerController.instance.gameObject.GetComponent<LifeController>()._currentLife 
    //    / PlayerController.instance.gameObject.GetComponent<LifeController>()._maxLife;
    //}

    public void OpenCloseMenu(GameObject menu) => menu.SetActive(!menu.activeInHierarchy);

    public void GameOver() => OpenCloseMenu(gameOverMenu);

    public void ChangeScene(string newScene) => SceneManagerScript.instance.LoadNewScene(newScene);

    public void ExitGame() => SceneManagerScript.instance.Exit();
}
