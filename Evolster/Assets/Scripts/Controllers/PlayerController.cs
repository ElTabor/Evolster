using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] public ActorStats playerStats;
    
    private LifeController _lifeController;
    public ManaController manaController;
    public SpellController spellController;
    public CurrencyController currencyController;
    public AbilityController abilityController;
    private Renderer _renderer;

    [SerializeField] private GameObject player;
    public Rigidbody2D rb;
    public float currentSpeed;
    public float speedUpgrade;
    public float currentDamage;
    public bool isBuffed;

    private float _horizontal;
    private float _vertical;
    private Vector2 _direction;
    private Animator _animator;


    [SerializeField] public GameObject uniqueAbilityPrefab;
    public UniqueAbilityData uniqueAbilityData;
    
    [SerializeField] public GameObject gameOverScreen;

    [SerializeField] private GameObject playerGo;

    [SerializeField] public bool uniqueAbilityIsAvailable = false;
    [SerializeField] GameObject[] abilitiesAvailable;
    private int currentAbilityIndex;
    public Transform aim;

    public PlayerController(float movementSpeed, GameObject player, float horizontal, GameObject shootingPoint)
    {
        playerStats.movementSpeed = movementSpeed;
        this.player = player;
        _horizontal = horizontal;
        spellController.shootingPoint = shootingPoint;
    }

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        _animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody2D>();
        _lifeController = GetComponent<LifeController>();
        manaController = GetComponent<ManaController>();
        spellController = GetComponentInChildren<SpellController>();
        _renderer = GetComponent<Renderer>();
        currencyController = GetComponent<CurrencyController>();
        abilityController = GetComponentInChildren<AbilityController>();

        currentSpeed = playerStats.movementSpeed;
        currentDamage = playerStats.damage;
        _lifeController.SetMaxLife(playerStats.maxLife);
        manaController.SetMaxMana(playerStats.maxMana);

        abilityController.SetAbility(abilitiesAvailable[currentAbilityIndex].GetComponent<IAbility>());
    }

    private void Update()
    {
        _renderer.enabled = (SceneManager.instance.scene != "Main Menu");
        if(_renderer.enabled) GetInput();

        if (SceneManager.instance.scene == "Lobby") currentSpeed = 4;
        else currentSpeed = playerStats.movementSpeed + speedUpgrade;

        Move();
        
        if(GameManager.instance.onLevel)
        {
            aim.gameObject.SetActive(uniqueAbilityIsAvailable);
            if(aim.gameObject.activeInHierarchy) 
                aim.position = Camera.main.ScreenToWorldPoint(new Vector3(
                    Input.mousePosition.x, 
                    Input.mousePosition.y, 
                    -Camera.main.transform.position.z));
        }

    }

    private void Move()
    {
        _direction = new Vector2(_horizontal, _vertical).normalized;
        rb.MovePosition(rb.position + _direction * (currentSpeed * Time.fixedDeltaTime));
        _animator.SetFloat("isWalking", _direction.sqrMagnitude);

        float direction;
        if(!GameManager.instance.gamePaused)
        {
            if (Input.GetAxisRaw("Horizontal") == 0) direction = 1;
            else direction = Input.GetAxisRaw("Horizontal");
            transform.localScale = new Vector2(direction, transform.localScale.y);
        }
    }

    private void GetInput()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        //if (Input.GetMouseButtonDown(0)) TryCastAbility();
        //if (Input.GetMouseButton(0)) TryCastAbility();

        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentAbilityIndex--;
            if (currentAbilityIndex < 0) currentAbilityIndex = abilitiesAvailable.Length - 1;
            abilityController.SetAbility(abilitiesAvailable[currentAbilityIndex].GetComponent<IAbility>());
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            currentAbilityIndex++;
            if (currentAbilityIndex > abilitiesAvailable.Length-1) currentAbilityIndex = 0;
            abilityController.SetAbility(abilitiesAvailable[currentAbilityIndex].GetComponent<IAbility>());
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) spellController.ChooseSpell(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) spellController.ChooseSpell(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) spellController.ChooseSpell(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) spellController.ChooseSpell(3);
    }
    
    public void EquipAbility(GameObject ability)
    {
        uniqueAbilityPrefab = ability;
    }

    //public void TryCastAbility()
    //{
    //    if (uniqueAbilityIsAvailable && !GameManager.instance.gamePaused && GameManager.instance.onLevel) CastAbility();
    //}

    //private void CastAbility()
    //{
    //    var aimDirection = (aim.position - transform.position).normalized;
    //    GameObject uniqueAbility = Instantiate(uniqueAbilityPrefab, transform.position + aimDirection*2, Quaternion.identity);
    //    //GameObject uniqueAbility = Instantiate(uniqueAbilityPrefab, transform);
    //    uniqueAbility.GetComponent<AreaAbility>().direction = aimDirection;
    //    //uniqueAbility.GetComponent<RayAbility>().dealingDamage = true;
    //    uniqueAbility.GetComponent<AreaAbility>().currentDamage += currentDamage;
    //    manaController.ManageMana(-uniqueAbilityPrefab.GetComponent<AreaAbility>().uniqueAbilityData.manaCost);

    //    //Debugs
    //    //if (cast.collider != null && cast.collider.gameObject.CompareTag("Enemy")) Debug.Log("cast Enemy!");
    //    Debug.DrawRay(transform.position, aimDirection * 1000000f, Color.red);
    //    Debug.Log("Ability Cast");
    //}

    public void Die()
    {
        UIManager.instance.GameOver();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, playerStats.attackRange);
    }
}