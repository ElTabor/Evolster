using UnityEngine;

public class SpellScript : MonoBehaviour
{
    [SerializeField] public SpellsData spellsData;

    public Vector2 direction;

    float creationTime;
    
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        creationTime = Time.time;
        rb.velocity = direction * spellsData.spellSpeed;
    }

    void Update()
    {
        if (Time.time >= creationTime + spellsData.spellLifeTime) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyScript>().GetDamage(spellsData.spellDamage);
        }
        Destroy(gameObject);
    }
}