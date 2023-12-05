using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagePopUp : MonoBehaviour
{
    [SerializeField] TextMeshPro _tmp;
    [SerializeField] private float dissapearingSpeed;
    private float startTime;
    [SerializeField] private float feedbackTime;

    private void Start() => startTime = Time.time;

    private void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime);
        
        if(Time.time >= startTime + feedbackTime)
            _tmp.alpha -= dissapearingSpeed * Time.deltaTime;

        if (_tmp.alpha < 0)
            Destroy(gameObject);
    }

    public void SetUp(float damageReceived, bool isCritical)
    {
        Color textColor;
        if (isCritical) textColor = Color.red;
        else textColor = Color.white;
        _tmp.text = damageReceived.ToString();
        _tmp.color = textColor;
    }

}
