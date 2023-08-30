using System.Collections;
using System.Collections.Generic;
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
    }

    public void LoadNewScene(string newScene)
    {
        SceneManager.LoadScene(newScene);
        scene = newScene;
    }
}
