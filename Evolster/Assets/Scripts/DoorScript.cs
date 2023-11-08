using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private string destination;
    [SerializeField] private Transform referencePoint;

    private void Update()
    {
        if(Physics2D.OverlapBox(referencePoint.position, referencePoint.localScale, 0).CompareTag("Player") && Input.GetKeyDown(KeyCode.Space))
            GameManager.instance.ChangeScene(destination);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(referencePoint.position, referencePoint.localScale);
    }
}
