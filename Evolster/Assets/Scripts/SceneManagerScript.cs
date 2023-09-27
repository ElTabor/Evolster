using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    static public SceneManagerScript instance;
    public string scene;

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        scene = SceneManager.GetActiveScene().name;
    }

    public void LoadNewScene(string newScene)
    {
        scene = newScene;
        SceneManager.LoadScene(newScene);
    }

    public void LoadMainMenu() => LoadNewScene("Main Menu");

    public void Exit() => Application.Quit();
}
