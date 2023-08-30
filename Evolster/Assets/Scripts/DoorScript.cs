using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] string destination;
    [SerializeField] Transform referencePoint;

    void Update()
    {
        if(Physics2D.OverlapBox(referencePoint.position, referencePoint.localScale, 0).CompareTag("Player") && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManagerScript.instance.LoadNewScene(destination);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(referencePoint.position, referencePoint.localScale);
    }
}
