using UnityEngine;

public class SpellScript : MonoBehaviour
{
    [SerializeField] public SpellsData spellData;

    public Vector2 direction;
    public float currentDamage;

    float creationTime;
    
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        creationTime = Time.time;
        rb.velocity = direction * spellData.spellSpeed;
        currentDamage = spellData.spellDamage;
    }

    void Update()
    {
        if (Time.time >= creationTime + spellData.spellLifeTime) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((gameObject.layer == LayerMask.NameToLayer("FriendlySpells") && collision.CompareTag("Enemy")) || 
            (gameObject.layer == LayerMask.NameToLayer("EnemySpells") && collision.CompareTag("Player")))
                collision.GetComponent<LifeController>().UpdateLife(currentDamage);
        Destroy(gameObject);
    }
}