using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    static public PlayerController instance;

    GameObject player;
    Rigidbody2D rb;
    [SerializeField] float speed;
    [SerializeField] float life;


    float horizontal;
    float vertical;
    Vector2 direction;


    Vector2 aimDirection;
    [SerializeField] GameObject gun;
    [SerializeField] GameObject shootingPoint;
    float lastAttack;
    [SerializeField] float attackCooldown;


    [SerializeField] GameObject spellPrefab;

    void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetInput();
        
        #region Movement

        direction = new Vector2(horizontal, vertical).normalized;        

        #endregion

        #region Aiming
        if(direction == new Vector2(1, 0))
        {
            gun.transform.localScale = new Vector3(1, 1, 1);
            gun.transform.rotation = Quaternion.Euler(new Vector3(gun.transform.rotation.x, gun.transform.rotation.y, 0));      //Aim right
        }
        if (direction == new Vector2(1, 1))
        {
            gun.transform.rotation = Quaternion.Euler(new Vector3(gun.transform.rotation.x, gun.transform.rotation.y, 45));      //Aim up-right
        }
        if (direction == new Vector2(0, 1))
        {
            gun.transform.rotation = Quaternion.Euler(new Vector3(gun.transform.rotation.x, gun.transform.rotation.y, 90));      //Aim up
        }
        if (direction == new Vector2(-1, 1))
        {
            gun.transform.rotation = Quaternion.Euler(new Vector3(gun.transform.rotation.x, gun.transform.rotation.y, 135));      //Aim up-left
        }
        if (direction == new Vector2(-1, 0))
        {
            gun.transform.localScale = new Vector3(1, -1, 1);
            gun.transform.rotation = Quaternion.Euler(new Vector3(gun.transform.rotation.x, gun.transform.rotation.y, 180));      //Aim left
        }
        if (direction == new Vector2(-1, -1))
        {
            gun.transform.rotation = Quaternion.Euler(new Vector3(gun.transform.rotation.x, gun.transform.rotation.y, 225));      //Aim down-left
        }
        if (direction == new Vector2(0, -1))
        {
            gun.transform.rotation = Quaternion.Euler(new Vector3(gun.transform.rotation.x, gun.transform.rotation.y, 270));      //Aim down
        }
        if (direction == new Vector2(1, -1))
        {
            gun.transform.rotation = Quaternion.Euler(new Vector3(gun.transform.rotation.x, gun.transform.rotation.y, 315));      //Aim down-right
        }

        #endregion

        #region Shooting


        if (SceneManagerScript.instance.scene != "Lobby" && Time.time >= lastAttack + attackCooldown) Attack();

        #endregion
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);
    }

    void GetInput()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if(direction != Vector2.zero) aimDirection = direction;
    }

    void Attack()
    {
        GameObject spell = Instantiate(spellPrefab, shootingPoint.transform.position, gun.transform.rotation);
        spell.GetComponent<SpellScript>().direction = aimDirection;
        lastAttack = Time.time;
    }

    public void UpdateLife(float lifeUpdate)
    {
        life += lifeUpdate;
        if (life <= 0) Destroy(gameObject);
    }
}