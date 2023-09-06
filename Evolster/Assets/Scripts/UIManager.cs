using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject skillCanvas;

    public GameObject spellSelectorCanvas;
    public GameObject spellSelector;

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

    public void MoveSpellSelector(int direction)
    {
        if (!spellSelectorCanvas.activeInHierarchy) spellSelectorCanvas.SetActive(true);

        spellSelector.transform.position = new Vector2(spellSelector.transform.position.x + 100 * direction, spellSelector.transform.position.y);

        //yield return new WaitForSeconds(3f);
        //spellSelectorCanvas.SetActive(false);
    }
}
