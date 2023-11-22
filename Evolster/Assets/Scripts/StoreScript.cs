using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StoreScript : MonoBehaviour
{
    public bool playerOnPosition;
    [SerializeField] Transform referencePoint;

    void Update()
    {
        playerOnPosition = Physics2D.OverlapCircle(referencePoint.position, referencePoint.localScale.x);
        if (playerOnPosition && Input.GetKeyDown(KeyCode.Space)) UIManager.instance.SetStoreCanvas();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(referencePoint.position, referencePoint.localScale.x);
    }
}
