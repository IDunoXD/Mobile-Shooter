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
    const float MinSpawnCooldown = 2;
    Energy playerEnergy;
    Health playerHealth;
    int kills=0;
    public static UnityAction<int, int> OnEnemyKill;
    public static UnityAction Ultimate;
    float StartTimerTime;
    void Awake()
    {
        playerEnergy = player.gameObject.GetComponent<Energy>();
        playerHealth = player.gameObject.GetComponent<Health>();
    }
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
            if(SpawnCooldown > MinSpawnCooldown){
                SpawnCooldown--;
            }
        }
    }
    void OnEnable(){
        OnEnemyKill += EnemyKill;
        Ultimate += KillAllEnemies;
    }
    void OnDisable(){
        OnEnemyKill -= EnemyKill;
        Ultimate -= KillAllEnemies;
    }
    void EnemyKill(int enemyId, int modifier){
        kills++;
        Debug.Log("kills: " + kills + ", enemyID: " + enemyId + ", modifier: " + modifier);
        //Add energy points to player depending on enemy id
        switch(enemyId){
            case 0:
                playerEnergy.AddEnergy(50);
                break;
            case 1:
                playerEnergy.AddEnergy(15);
                break;
            default:
                break;
        }
        //Process modifier kills
        switch(modifier){
            case 0:
                break;
            case 1:
                playerHealth.Heal(100);
                break;
            default:
                break;
        }
    }
    void KillAllEnemies(){
        for(int i = 0; i < transform.childCount; i++){
            Destroy(transform.GetChild(i).gameObject);
        }
    }
    void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(spawner.position, 0.1f);
    }
}
