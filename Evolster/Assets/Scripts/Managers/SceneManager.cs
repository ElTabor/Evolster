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
        UnityEngine.SceneManagement.SceneManager.LoadScene(newScene);
        scene = newScene;

    }

    public void LoadMainMenu() => LoadNewScene("Main Menu");

    public void Exit() => Application.Quit();
}
