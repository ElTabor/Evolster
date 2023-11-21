using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private string destination;
    [SerializeField] private Transform referencePoint;
    bool playerIsNear;

    private void Update()
    {
        if (playerIsNear && Input.GetKeyDown(KeyCode.Space))
            GameManager.instance.ChangeScene(destination);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(referencePoint.position, referencePoint.localScale);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) playerIsNear = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) playerIsNear = false;
    }
}
