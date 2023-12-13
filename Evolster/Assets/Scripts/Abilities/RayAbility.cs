using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class RayAbility : MonoBehaviour, IAbility
{
    [field:SerializeField] public UniqueAbilityData abilityData { get; set; }
    public float currentDamage { get; set; }
    [field: SerializeField] public LayerMask enemiesLayer { get; set; }

    GameObject raySprite;
    private float lastDamageDealtTime;
    public bool dealingDamage;
    [SerializeField] GameObject rayPrefab;

    AudioSource source;

    public void Start()
    {
        currentDamage = abilityData.uniqueAbilityDamage;
        lastDamageDealtTime = Time.time;
        raySprite = Instantiate(rayPrefab, PlayerController.instance.transform.position, Quaternion.identity);
        raySprite.SetActive(false);
        source = GetComponent<AudioSource>();
    }

    public void Update()
    {
        source.volume = AudioController.instance.sfxVolume;
        source.Play();
        if (PlayerController.instance.abilityController.abilityAvailable  && !GameManager.instance.gamePaused && GameManager.instance.onLevel && Input.GetMouseButton(0))
            CastAbility();
        else dealingDamage = false;

        raySprite.SetActive(dealingDamage);
    }

    public void CastAbility()
    {
        dealingDamage = true;
        //Calculat ray's direction & origin
        Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - PlayerController.instance.transform.position).normalized;
        Vector3 origin = new Vector2(PlayerController.instance.transform.position.x, PlayerController.instance.transform.position.y);

        //Cast ray
        RaycastHit2D ray = Physics2D.Raycast(origin, direction, 100, enemiesLayer);

        //Create asset
        raySprite.transform.position = PlayerController.instance.transform.position;
        float angulo = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        raySprite.transform.rotation = Quaternion.Euler(0, 0, angulo);
        float sizeX;
        if (ray.collider != null) sizeX = ray.distance;
        else sizeX = 100;
            raySprite.GetComponent<SpriteRenderer>().size = new Vector2(sizeX, raySprite.GetComponent<SpriteRenderer>().size.y);

        //Debug
        Debug.DrawRay(origin, direction * 100, Color.red);

        //Deal Damage
        if (Time.time >= lastDamageDealtTime + abilityData.cooldown)
        {
            if (ray.collider != null && ray.collider.gameObject.CompareTag("Enemy")) ray.collider.gameObject.GetComponent<LifeController>().UpdateLife(currentDamage, true);
            GameManager.instance.player.GetComponent<ManaController>().ManageMana(-abilityData.manaCost);
            lastDamageDealtTime = Time.time;
        }
    }
}
