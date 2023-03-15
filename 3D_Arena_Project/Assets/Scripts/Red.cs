using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Red : MonoBehaviour
{
    NavMeshAgent agent;
    Health health;
    [SerializeField] int damage;
    [SerializeField] int modifier = 0;
    [SerializeField] int enemyId = 1;
    public Transform target;
	public LayerMask DestroyOnContactLayer;

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
        EnemiesManager.OnEnemyKill(enemyId, modifier);
        Destroy(gameObject);
    }   
    void OnTriggerEnter(Collider other){
        //Debug.Log(other.gameObject.layer);
        if(DestroyOnContactLayer == (DestroyOnContactLayer | (1 << other.gameObject.layer))){
            if(other.gameObject.TryGetComponent(out Health health)){
                health.GetDamage(damage, modifier);
            }
            Destroy(gameObject);
        }
    }
}