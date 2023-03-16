using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float health;
    public float MaxHealth;
    public UnityAction<int> Die;
    public UnityAction<float, float> HealthUpdate;
    public void GetDamage(float damage, int modifier){
        health -= damage;
        if(health <= 0){
            Die?.Invoke(modifier);
        }
        health = Mathf.Clamp(health, 0, MaxHealth);
        HealthUpdate?.Invoke(MaxHealth, health);
    }
    public void Heal(float value){
        health += value;
        health = Mathf.Clamp(health, 0, MaxHealth);
        HealthUpdate?.Invoke(MaxHealth, health);
    }
}
