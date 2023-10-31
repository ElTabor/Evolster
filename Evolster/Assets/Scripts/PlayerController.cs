using UnityEngine;
using System.Linq;
using UnityEngine.Profiling.Experimental;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    LifeController lifeController;
    public ManaController manaController;
    public SpellController spellController;

    [SerializeField] private GameObject player;
    public Rigidbody2D _rb;
    [SerializeField] public StatsData _stats;
    public float currentSpeed;
    public float currentDamage;
    public bool isBuffed;

    private float _horizontal;
    private float _vertical;
    private Vector2 _direction;


    [SerializeField] private GameObject uniqueAbilityPrefab;
    
    [SerializeField] public GameObject gameOverScreen;

    [SerializeField] GameObject playerGO;

    [SerializeField] private bool uniqueAbilityIsAvailable = false;
    [SerializeField] private Transform aim;

    public PlayerController(float speed, GameObject player, float horizontal, GameObject shootingPoint)
    {
        _stats.speed = speed;
        this.player = player;
        _horizontal = horizontal;
        spellController.shootingPoint = shootingPoint;
    }

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        player = GameObject.FindGameObjectWithTag("Player");
        _rb = player.GetComponent<Rigidbody2D>();
        lifeController = GetComponent<LifeController>();
        manaController = GetComponent<ManaController>();
        spellController = GetComponentInChildren<SpellController>();

        currentSpeed = _stats.speed;
        currentDamage = _stats.damage;
        lifeController.SetMaxLife(_stats.maxLife);
        manaController.SetMaxMana(_stats.maxMana);
    }

    void Update()
    {
        GetInput();

        #region Movement

        _direction = new Vector2(_horizontal, _vertical).normalized;

        #endregion

        if (uniqueAbilityIsAvailable)
        {
            aim.position = Camera.main.ScreenToWorldPoint(new Vector3(
                Input.mousePosition.x,
                Input.mousePosition.y,
                -Camera.main.transform.position.z));
        }
    }



    private void FixedUpdate()
    {
        _rb.MovePosition(_rb.position + _direction * (currentSpeed * Time.fixedDeltaTime));
    }

    private void GetInput()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        _vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButtonDown(0)) TryCastAbility(uniqueAbilityPrefab.GetComponent<UniqueAbilityScript>());

        if (Input.GetKeyDown(KeyCode.Alpha1)) spellController.ChooseSpell(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) spellController.ChooseSpell(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) spellController.ChooseSpell(2);
        if (Input.GetKeyDown(KeyCode.Alpha4)) spellController.ChooseSpell(3);
    }
    
    public void TryCastAbility(UniqueAbilityScript abilityToCast)
    {
        if (manaController.currentMana >= abilityToCast.uniqueAbilityData.manaCost) CastAbility();
    }

    public void CastAbility()
    {
        var aimDirection = (aim.position - transform.position).normalized;
        RaycastHit2D cast = Physics2D.Raycast(transform.position, (aim.position - transform.position), 1000000f);
        GameObject uniqueAbility = Instantiate(uniqueAbilityPrefab, cast.transform.position + aimDirection * 2, Quaternion.identity);
        uniqueAbility.GetComponent<UniqueAbilityScript>().direction = aimDirection;
        uniqueAbility.GetComponent<UniqueAbilityScript>().currentDamage += currentDamage;
        manaController.ManageMana(-uniqueAbility.GetComponent<UniqueAbilityScript>().uniqueAbilityData.manaCost);

        //Debugs
        if (cast.collider != null && cast.collider.gameObject.CompareTag("Enemy")) Debug.Log("cast Enemy!");
        Debug.DrawRay(transform.position, (aim.position - transform.position), Color.red);
        Debug.Log("Ability Cast");
    }

    public void Die()
    {
        UIManager.instance.GameOver();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, _stats.attackRange);
    }
}