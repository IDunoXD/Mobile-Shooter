using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class Red : MonoBehaviour
{
    NavMeshAgent agent;
    [SerializeField] Health health;
    [SerializeField] int damage;
    [SerializeField] int modifier = 0;
    [SerializeField] int enemyId = 1;
    [SerializeField] float FlyUpHeight = 1.5f;
    [SerializeField] float FlyUpTime = 1; //time to fly up
    [SerializeField] float DIstanceToGlideDown = 3;
    [SerializeField] float VerticalGlidingSpeed = 2; //units per second
    [SerializeField] Transform Eyes;
    float FlightHeight;
    float FlightTime;
    float PrevDistance;
    float CurrentDistance;
    public Transform target;
	public LayerMask DestroyOnContactLayer;
    enum RedState { FylUp, Charge };
    RedState redState;

	void OnEnable(){
        health.Die += Die;
    }
    void OnDisable(){
        health.Die -= Die;
    }
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        redState = RedState.FylUp;
        FlightTime = 0;
    }
    void Update()
    {
        switch(redState){
            case RedState.FylUp:
                FlightHeight = Mathf.Lerp(0, FlyUpHeight, FlightTime/FlyUpTime);
                Eyes.position = transform.position + Vector3.up * FlightHeight;
                FlightTime += Time.deltaTime;
                if(FlightHeight == FlyUpHeight){
                    redState = RedState.Charge;
                }
                break;
            case RedState.Charge:
                Eyes.LookAt(target);
                agent.SetDestination(target.position);
                CurrentDistance = Vector3.Distance(target.position, transform.position);
                if(CurrentDistance < DIstanceToGlideDown){
                    //Player running away faster
                    if(CurrentDistance > PrevDistance)
                        FlightHeight += VerticalGlidingSpeed * Time.deltaTime;
                    //Red is faster
                    else
                        FlightHeight -= VerticalGlidingSpeed * Time.deltaTime;
                }else{
                    FlightHeight += VerticalGlidingSpeed * Time.deltaTime;
                }
                FlightHeight = Mathf.Clamp(FlightHeight, 0, FlyUpHeight);
                Eyes.position = transform.position + Vector3.up * FlightHeight;
                PrevDistance = Vector3.Distance(target.position, transform.position);
                break;
        }
    }
    void Die(int modifier){
        EnemiesManager.OnEnemyKill(enemyId, modifier);
        Destroy(gameObject);
    }   
    public void OnTrigger(Collider other){
        //Debug.Log(other.gameObject.layer);
        if(DestroyOnContactLayer == (DestroyOnContactLayer | (1 << other.gameObject.layer))){
            if(other.gameObject.TryGetComponent(out Health health)){
                health.GetDamage(damage, modifier);
            }
            Destroy(gameObject);
        }
    }
}