using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    [SerializeField] int speed;
    [SerializeField] int damage;
    public Vector2 direction;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //direction = PlayerController.instance.aimDirection;
    }
    void Update()
    {
        rb.velocity = direction * speed;
    }
}
