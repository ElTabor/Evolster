using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoundsManager : MonoBehaviour
{
    public int enemiesOnScreen;
    public bool prepTime;
    void Start()
    {
        prepTime = true;
        StartCoroutine(prepTimeSet());
    }

    // Update is called once per frame
    void Update()
    {

        if(!prepTime)
        {
            enemiesOnScreen = GameObject.FindGameObjectsWithTag("Enemy").Count();

            if (enemiesOnScreen <= 0) SceneManagerScript.instance.LoadNewScene("Lobby");
        }
    }

    IEnumerator prepTimeSet()
    {
        yield return new WaitForSeconds(3f);
        prepTime = false;
    }
}
