using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject spellSelectionMenu;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;

    [SerializeField] Slider lifeBar;

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

        if (Input.GetKeyDown(KeyCode.Escape)) OpenCloseMenu(pauseMenu);

        UpdateLife();
    }

    private void UpdateLife() => lifeBar.value = PlayerController.instance.gameObject.GetComponent<LifeController>()._currentLife / PlayerController.instance.gameObject.GetComponent<LifeController>()._maxLife;

    public void OpenCloseMenu(GameObject menu) => menu.SetActive(!menu.activeInHierarchy);

    public void GameOver() => OpenCloseMenu(gameOverMenu);

    public void GoToMainMenu() => SceneManagerScript.instance.LoadNewScene("Main Menu");

    public void Exit() => Application.Quit();
}
