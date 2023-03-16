using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float RunFromPlayerRadius;
    Vector3 RandomDirection;
    NavMeshAgent agent;
    void Awake(){
        agent = GetComponent<NavMeshAgent>();
    }
    void Update()
    {
        if(Vector3.Distance(agent.destination, transform.position) < agent.stoppingDistance){
            RandomDirection = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
            agent.SetDestination(player.position + RandomDirection * RunFromPlayerRadius);
        }
    }
    void OnDrawGizmos(){
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(player.position + RandomDirection * RunFromPlayerRadius, 0.1f);
    }
}
