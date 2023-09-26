using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject skillCanvas;

    public GameObject spellSelectorCanvas;
    public GameObject spellSelector;

    public GameObject spellSelectionMenu;

    public GameObject pauseMenu;

    bool gamePaused;
    bool gameOver = false;

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        //gamePaused = spellSelectionMenu.activeInHierarchy;
        //if (gamePaused) Time.timeScale = 0f;
        //else Time.timeScale = 1f;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    //public void ToggleUICanvas(GameObject canvas)
    //{
    //    canvas.SetActive(!canvas.activeInHierarchy);
    //}

    public void MoveSpellSelector(int direction)
    {
        if (!spellSelectorCanvas.activeInHierarchy) spellSelectorCanvas.SetActive(true);

        spellSelector.transform.position = new Vector2(spellSelector.transform.position.x + 100 * direction, spellSelector.transform.position.y);

        //yield return new WaitForSeconds(3f);
        //spellSelectorCanvas.SetActive(false);
    }

    public void OpenCloseMenu(GameObject menu) => menu.SetActive(!menu.activeInHierarchy);

    public void GameOver()
    {
        gameOver = true;
        PlayerController.instance.gameOverScreen.SetActive(true);
    }

    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Exit() => Application.Quit();
}
