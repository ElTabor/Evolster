using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField] public ActorStats playerStats;
    
    private LifeController _lifeController;
    public ManaController manaController;
    public SpellController spellController;
    private Renderer _renderer;

    [SerializeField] private GameObject player;
    public Rigidbody2D rb;
    public float currentSpeed;
    public float currentDamage;
    public bool isBuffed;

    private float _horizontal;
    private float _vertical;
    private Vector2 _direction;


    [SerializeField] public GameObject uniqueAbilityPrefab;
    
    [SerializeField] public GameObject gameOverScreen;

    [SerializeField] private GameObject playerGo;

    [SerializeField] public bool uniqueAbilityIsAvailable = false;
    [SerializeField] private Transform aim;

    public int currency;

    public PlayerController(float movementSpeed, GameObject player, float horizontal, GameObject shootingPoint)
    {
        playerStats.movementSpeed = movementSpeed;
        this.player = player;
        _horizontal = horizontal;
        spellController.shootingPoint = shootingPoint;
    }

    private void Start()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        _lifeController = GetComponent<LifeController>();
        manaController = GetComponent<ManaController>();
        spellController = GetComponentInChildren<SpellController>();
        _renderer = GetComponent<Renderer>();

        currentSpeed = playerStats.movementSpeed;
        currentDamage = playerStats.damage;
        _lifeController.SetMaxLife(playerStats.maxLife);
        manaController.SetMaxMana(playerStats.maxMana);
    }

    private void Update()
    {
        _renderer.enabled = (SceneManager.instance.scene != "Main Menu");
        if(_renderer.enabled) GetInput();

        #region Movement

        _direction = new Vector2(_horizontal, _vertical).normalized;

        #endregion

        aim.gameObject.SetActive(uniqueAbilityIsAvailable);
        if(aim.gameObject.activeInHierarchy) 
            aim.position = Camera.main.ScreenToWorldPoint(new Vector3(
                Input.mousePosition.x, 
                Input.mousePosition.y, 
                -Camera.main.transform.position.z));

        rb.MovePosition(rb.position + _direction * (currentSpeed * Time.fixedDeltaTime));
    }

    private void GetInput()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0)) TryCastAbility();

        if (Input.GetKeyDown(KeyCode.Alpha1)) spellController.ChooseSpell(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) spellController.ChooseSpell(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) spellController.ChooseSpell(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) spellController.ChooseSpell(3);
    }
    
    public void TryCastAbility()
    {
        if (uniqueAbilityIsAvailable && !UIManager.instance.gamePaused) CastAbility();
    }

    private void CastAbility()
    {
        var aimDirection = (aim.position - transform.position).normalized;
        RaycastHit2D cast = Physics2D.Raycast(transform.position, aimDirection, 1000000f);
        GameObject uniqueAbility = Instantiate(uniqueAbilityPrefab, transform.position + aimDirection*2, Quaternion.identity);
        uniqueAbility.GetComponent<UniqueAbility>().direction = aimDirection;
        uniqueAbility.GetComponent<UniqueAbility>().currentDamage += currentDamage;
        manaController.ManageMana(-uniqueAbility.GetComponent<UniqueAbility>().uniqueAbilityData.manaCost);

        //Debugs
        if (cast.collider != null && cast.collider.gameObject.CompareTag("Enemy")) Debug.Log("cast Enemy!");
        Debug.DrawRay(transform.position, aimDirection * 1000000f, Color.red);
        Debug.Log("Ability Cast");
    }

    public void Die()
    {
        UIManager.instance.GameOver();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, playerStats.attackRange);
    }
}