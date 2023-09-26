using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject spellSelectionMenu;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;

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
    }

    public void OpenCloseMenu(GameObject menu) => menu.SetActive(!menu.activeInHierarchy);

    public void GameOver() => OpenCloseMenu(gameOverMenu);

    public void GoToMainMenu() => SceneManagerScript.instance.LoadNewScene("Main Menu");

    public void Exit() => Application.Quit();
}
