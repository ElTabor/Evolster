using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript instance;
    public string scene;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        scene = SceneManager.GetActiveScene().name;
    }

    public void LoadNewScene(string newScene)
    {
        scene = newScene;
        SceneManager.LoadScene(newScene);
        if (newScene == "Main Menu") Restart();
        PlayerController.instance.transform.position = Vector3.zero;
    }

    void Restart()
    {
        PlayerController.instance.GetComponent<LifeController>().SetMaxLife(PlayerController.instance.stats.maxLife);
        PlayerController.instance.GetComponent<ManaController>().SetMaxMana(PlayerController.instance.stats.maxMana);
        UIManager.instance._timeElapsed = 0;
    }

    public void LoadMainMenu() => LoadNewScene("Main Menu");

    public void Exit() => Application.Quit();
}
