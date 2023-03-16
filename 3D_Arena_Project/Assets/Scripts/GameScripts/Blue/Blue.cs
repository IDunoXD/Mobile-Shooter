using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Blue : MonoBehaviour
{
    NavMeshAgent agent;
    Health health;
    [SerializeField] int enemyId = 0;
    [SerializeField] Transform GunOrigin;
    [SerializeField] float shootCooldown = 3; //seconds
    [SerializeField] GameObject enemyBullet;
    float StartTimerTime;
    public Transform target;
    void OnEnable(){
        health.Die += Die;
    }
    void OnDisable(){
        health.Die -= Die;
    }
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        health = GetComponent<Health>();
    }
    void Start()
    {
        StartTimerTime = Time.time;
    }
    void Update()
    {
        GunOrigin.LookAt(target);
        agent.SetDestination(target.position);
        if(Time.time - StartTimerTime > shootCooldown){
            var bullet = Instantiate(enemyBullet, GunOrigin.position, GunOrigin.rotation, BulletContainer.bulletContainer);
            bullet.GetComponent<EnemyBullet>().target = target;
            StartTimerTime = Time.time;
        }
    }
    void Die(int modifier){
        EnemiesManager.OnEnemyKill(enemyId, modifier);
        Destroy(gameObject);
    }
}
