using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject skillCanvas;

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }
    public void ToggleUICanvas(GameObject canvas)
    {
        canvas.SetActive(!canvas.activeInHierarchy);
    }
}
