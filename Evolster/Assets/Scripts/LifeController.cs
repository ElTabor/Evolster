using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{
    [SerializeField] public float _currentLife;
    [SerializeField] public float _maxLife;

    public void SetMaxLife(float maxLife)
    {
        _maxLife = maxLife;
        _currentLife = _maxLife;
    }

    public void GetDamage(float damageReceived)
    {
        _currentLife -= damageReceived;
        if (_currentLife > _maxLife) _currentLife = _maxLife;
        if (_currentLife <= 0)
        {
            if (gameObject.tag == "Player") PlayerController.instance.Die();
            else if (gameObject.tag == "Enemy")
            {
                int r = Random.Range(0, 100);
                if(r % 2 == 0) BuffsManager.instance.SetSpawnPosition(gameObject.transform.position);
                Destroy(gameObject);
            }
            else Destroy(gameObject);
        }
    }
}