using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    public Image lifeBar;
    public float currentLife;
    public float maxLife;

    private void Update()
    {
        lifeBar.fillAmount = currentLife / maxLife;
    }
}
