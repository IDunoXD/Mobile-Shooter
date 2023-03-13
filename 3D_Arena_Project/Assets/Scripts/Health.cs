using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float health;
    public float MaxHealth;
    public UnityAction<int> Die;
    public void GetDamage(float damage, int modifier){
        health-=damage;
        if(health <= 0){
            Die?.Invoke(modifier);
        }
        health = Mathf.Clamp(health, 0, MaxHealth);
    }
}
