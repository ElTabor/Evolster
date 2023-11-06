using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager instance;
    public string scene;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        scene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }

    public void LoadNewScene(string newScene)
    {
        scene = newScene;
        UnityEngine.SceneManagement.SceneManager.LoadScene(newScene);
        if (newScene == "Main Menu") Restart();
        PlayerController.Instance.transform.position = Vector3.zero;
    }

    void Restart()
    {
        PlayerController.Instance.GetComponent<LifeController>().SetMaxLife(PlayerController.Instance.playerStats.maxLife);
        PlayerController.Instance.GetComponent<ManaController>().SetMaxMana(PlayerController.Instance.playerStats.maxMana);
        UIManager.instance._timeElapsed = 0;
    }

    public void LoadMainMenu() => LoadNewScene("Main Menu");

    public void Exit() => Application.Quit();
}
