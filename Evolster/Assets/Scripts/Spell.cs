using UnityEngine;

public class Spell : MonoBehaviour
{
    [SerializeField] public SpellsData spellData;

    public Vector2 direction;
    public float currentDamage;

    private float _creationTime;

    private Rigidbody2D _rb;
    private AudioSource _source;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _source = GetComponent<AudioSource>();
        _source.PlayOneShot(_source.clip, AudioController.instance.sfxVolume);
        _creationTime = Time.time;
        _rb.velocity = direction * spellData.spellSpeed;
    }

    private void Update()
    {
        if (Time.time >= _creationTime + spellData.spellLifeTime) Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((gameObject.layer == LayerMask.NameToLayer("FriendlySpells") && collision.CompareTag("Enemy")) ||
            (gameObject.layer == LayerMask.NameToLayer("EnemySpells") && collision.CompareTag("Player")))
        {
            collision.GetComponent<LifeController>().UpdateLife(currentDamage, false);
            Debug.Log(collision.gameObject.name + "daño " + currentDamage);
        }
        Destroy(gameObject);
    }
}