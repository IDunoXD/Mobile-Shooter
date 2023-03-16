using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Health health;
    [SerializeField] Gradient gradient;
    [SerializeField] RectTransform barOrigin;
    [SerializeField] Image healthImage;
    [SerializeField] TMP_Text healthText;
    Vector3 updatedScale;
    float healthPercentage;
    void Start(){
        SetHealth(health.MaxHealth, health.health);
    }
    void OnEnable(){
        health.HealthUpdate += SetHealth;
    }
    void OnDisable(){
        health.HealthUpdate -= SetHealth;
    }
    public void SetHealth(float max, float curent){
        healthPercentage = curent/max;
        updatedScale = barOrigin.localScale;
        updatedScale.x = healthPercentage;

        barOrigin.localScale = updatedScale;
        healthImage.color = gradient.Evaluate(healthPercentage);
        healthText.text = curent + "/" + max;
    }
}
