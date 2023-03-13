using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemiesManager : MonoBehaviour
{
    [SerializeField] GameObject red;
    [SerializeField] GameObject blue;
    [SerializeField] Transform spawner;
    [SerializeField] Transform player;
    [SerializeField] float SpawnCooldown = 5; //seconds
    int kills=0;
    public static UnityAction<int> OnEnemyKill;
    float StartTimerTime;
    void Srart()
    {
        StartTimerTime = Time.time;
    }
    void Update()
    {
        if(Time.time - StartTimerTime > SpawnCooldown){
            //20% to spawn Blue, 80% to spawn Red
            if(Random.Range(0, 100) < 20){
                var Enemy = Instantiate(blue, spawner.position, spawner.rotation, transform);
                Enemy.GetComponent<Blue>().target = player;
            }else{
                var Enemy = Instantiate(red, spawner.position, spawner.rotation, transform);
                Enemy.GetComponent<Red>().target = player;
            }
            StartTimerTime = Time.time;
        }
    }
    void OnEnable(){
        OnEnemyKill += KillCount;
    }
    void OnDisable(){
        OnEnemyKill -= KillCount;
    }
    void KillCount(int modifier){
        kills++;
        Debug.Log("kills: " + kills + ", modifier: " + modifier);
    }
    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spawner.position, 0.1f);
    }
}
