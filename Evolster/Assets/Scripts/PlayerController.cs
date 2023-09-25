using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] private GameObject player;
    private Rigidbody2D _rb;
    [SerializeField] private float speed;
    [SerializeField] private float life;


    private float _horizontal;
    private float _vertical;
    private Vector2 _direction;


    private Vector2 _aimDirection;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject shootingPoint;
    private float _lastAttack;
    [SerializeField] private float attackCooldown;


    [SerializeField] private GameObject spellPrefab;

    public PlayerController(float speed, GameObject player, float horizontal, GameObject shootingPoint)
    {
        this.speed = speed;
        this.player = player;
        _horizontal = horizontal;
        this.shootingPoint = shootingPoint;
    }

    private void Start()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        player = GameObject.FindGameObjectWithTag("Player");
        _rb = player.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        GetInput();
        
        #region Movement

        _direction = new Vector2(_horizontal, _vertical).normalized;        

        #endregion

        #region Aiming
        if(_direction == new Vector2(1, 0))
        {
            gun.transform.localScale = new Vector3(1, 1, 1);
            gun.transform.rotation = Quaternion.Euler(new Vector3(gun.transform.rotation.x, gun.transform.rotation.y, 0));      //Aim right
        }
        if (_direction == new Vector2(1, 1))
        {
            gun.transform.rotation = Quaternion.Euler(new Vector3(gun.transform.rotation.x, gun.transform.rotation.y, 45));      //Aim up-right
        }
        if (_direction == new Vector2(0, 1))
        {
            gun.transform.rotation = Quaternion.Euler(new Vector3(gun.transform.rotation.x, gun.transform.rotation.y, 90));      //Aim up
        }
        if (_direction == new Vector2(-1, 1))
        {
            gun.transform.rotation = Quaternion.Euler(new Vector3(gun.transform.rotation.x, gun.transform.rotation.y, 135));      //Aim up-left
        }
        if (_direction == new Vector2(-1, 0))
        {
            gun.transform.localScale = new Vector3(1, -1, 1);
            gun.transform.rotation = Quaternion.Euler(new Vector3(gun.transform.rotation.x, gun.transform.rotation.y, 180));      //Aim left
        }
        if (_direction == new Vector2(-1, -1))
        {
            gun.transform.rotation = Quaternion.Euler(new Vector3(gun.transform.rotation.x, gun.transform.rotation.y, 225));      //Aim down-left
        }
        if (_direction == new Vector2(0, -1))
        {
            gun.transform.rotation = Quaternion.Euler(new Vector3(gun.transform.rotation.x, gun.transform.rotation.y, 270));      //Aim down
        }
        if (_direction == new Vector2(1, -1))
        {
            gun.transform.rotation = Quaternion.Euler(new Vector3(gun.transform.rotation.x, gun.transform.rotation.y, 315));      //Aim down-right
        }

        #endregion

        #region Shooting


        if (SceneManagerScript.instance.scene != "Lobby" && Time.time >= _lastAttack + attackCooldown) Attack();

        #endregion
    }

    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _direction * (speed * Time.fixedDeltaTime));
    }

    private void GetInput()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");
        if(_direction != Vector2.zero) _aimDirection = _direction;

        if(SceneManagerScript.instance.scene == "Lobby")
        {
            if (Input.GetKeyDown(KeyCode.Q)) UIManager.instance.MoveSpellSelector(-1);
            else if (Input.GetKeyDown(KeyCode.E)) UIManager.instance.MoveSpellSelector(1);
        }
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void Attack()
    {
        GameObject spell = Instantiate(spellPrefab, shootingPoint.transform.position, gun.transform.rotation);
        spell.GetComponent<SpellScript>().direction = _aimDirection;
        _lastAttack = Time.time;
    }

    public void UpdateLife(float lifeUpdate)
    {
        life += lifeUpdate;
        if (life <= 0) Destroy(gameObject);
    }

    public void GetReward(string reward)
    {
        Debug.Log(reward);
    }
}