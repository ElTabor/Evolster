using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class AreaAbilityBullet : MonoBehaviour
{
    public float damageArea;
    public float currentDamage;
    private Rigidbody2D _rb;
    public Vector2 direction;
    public float speed;
    public float lifeTime;
    private float startTime;
    Animator animator;
    public LayerMask enemiesLayer;
    public bool active;
    AudioSource source;
    [SerializeField] AudioClip[] clips;

    private void Start()
    {
        animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        source.PlayOneShot(clips[0], AudioController.instance.sfxVolume);
        startTime = Time.time;
    }

    private void Update()
    {
        if (Time.time >= startTime + lifeTime)
            Destroy(gameObject);


        if (!active) _rb.velocity = direction * speed;
        else _rb.velocity = Vector2.zero;
    }

    private void OnTriggerEnter2D()
    {
        if(!active)
        {
            DealDamage();
            source.PlayOneShot(clips[1], AudioController.instance.sfxVolume);
            source.PlayOneShot(clips[2], AudioController.instance.sfxVolume);
        }
    }

    public virtual void DealDamage()
    {
        active = true;
        animator.SetBool("Active", true);
        transform.rotation = Quaternion.AngleAxis(0, Vector3.zero);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, damageArea);
    }
}
