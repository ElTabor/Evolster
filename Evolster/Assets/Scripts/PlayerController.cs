using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    static public PlayerController instance;

    GameObject player;
    Rigidbody2D playerRB;
    [SerializeField] int speed;


    float horizontal;
    float vertical;
    Vector2 direction;
    public Vector2 aimDirection;


    [SerializeField] GameObject body;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject shootingPoint;


    [SerializeField] GameObject bulletPrefab;
    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        DontDestroyOnLoad(instance);

        player = GameObject.FindGameObjectWithTag("Player");
        playerRB = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetInput();
        #region Movement

        if (direction.x == 1) playerRB.velocity = new Vector2(1, playerRB.velocity.y) * speed;
        else if (direction.x == -1) playerRB.velocity = new Vector2(-1, playerRB.velocity.y) * speed;
        else playerRB.velocity = new Vector2(0, playerRB.velocity.y) * speed;

        if (direction.y == 1) playerRB.velocity = new Vector2(playerRB.velocity.x, 1) * speed;
        else if (direction.y == -1) playerRB.velocity = new Vector2(playerRB.velocity.x, -1)*speed;
        else playerRB.velocity = new Vector2(playerRB.velocity.x, 0) * speed;

        #endregion

        #region Aiming
        if(direction == new Vector2(1, 0))
        {
            gun.transform.localScale = new Vector3(1, 1, 1);
            gun.transform.rotation = Quaternion.Euler(new Vector3(gun.transform.rotation.x, gun.transform.rotation.y, 0));      //Aim right
        }
        if (direction == new Vector2(-1, 0))
        {
            gun.transform.localScale = new Vector3(1, -1, 1);
            gun.transform.rotation = Quaternion.Euler(new Vector3(gun.transform.rotation.x, gun.transform.rotation.y, 180));      //Aim left
        }
        if (direction == new Vector2(0, 1))
        {
            gun.transform.rotation = Quaternion.Euler(new Vector3(gun.transform.rotation.x, gun.transform.rotation.y, 90));      //Aim up
        }
        if (direction == new Vector2(0, -1))
        {
            gun.transform.rotation = Quaternion.Euler(new Vector3(gun.transform.rotation.x, gun.transform.rotation.y, 270));      //Aim down
        }

        #endregion
    }

    void GetInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector2(horizontal, vertical);

        if (Input.GetKeyDown(KeyCode.Space)) Shoot();
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.transform.position, gun.transform.rotation);
        bullet.GetComponent<bulletScript>().direction = Vector2.zero;
    }
}
