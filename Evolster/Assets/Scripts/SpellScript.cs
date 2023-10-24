using UnityEngine;

public class SpellScript : MonoBehaviour
{
    [SerializeField] public SpellsData spellsData;

    public Vector2 direction;
    public float currentDamage;

    float creationTime;
    
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        creationTime = Time.time;
        rb.velocity = direction * spellsData.spellSpeed;
        currentDamage = spellsData.spellDamage;
    }

    void Update()
    {
        if (Time.time >= creationTime + spellsData.spellLifeTime) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((gameObject.layer == LayerMask.NameToLayer("FriendlySpells") && collision.CompareTag("Enemy")) || 
            (gameObject.layer == LayerMask.NameToLayer("EnemySpells") && collision.CompareTag("Player")))
                collision.GetComponent<LifeController>().GetDamage(currentDamage);
        Destroy(gameObject);
    }
}