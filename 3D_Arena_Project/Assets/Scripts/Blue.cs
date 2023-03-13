using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Blue : MonoBehaviour
{
    NavMeshAgent agent;
    Health health;
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
    void Update()
    {
        agent.SetDestination(target.position);
    }
    void Die(int modifier){
        EnemiesManager.OnEnemyKill(modifier);
        Destroy(gameObject);
    }
}
