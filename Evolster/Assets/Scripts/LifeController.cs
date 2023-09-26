using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeController : MonoBehaviour
{
    [SerializeField] float _currentLife;
    float _maxLife;

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
            else Destroy(gameObject);
        }
    }
}