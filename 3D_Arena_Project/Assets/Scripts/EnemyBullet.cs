using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] LayerMask ignoreLayer;
    [SerializeField] float energyDamage;
    [SerializeField] int modifier;
    [SerializeField] float flySpeed;
    public Transform target;
    Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate(){
        transform.LookAt(target);
        transform.position += transform.forward * flySpeed * Time.fixedDeltaTime;
    }
    void OnCollisionEnter(Collision other){
        //Ignore collisions with layer (Shooter)
        if(ignoreLayer != (ignoreLayer | (1 << other.gameObject.layer))){
            if(other.gameObject.TryGetComponent(out Energy energy)){
                energy.RemoveEnergy(energyDamage);
            }
            //Destroy if hitting something else, or bounce didn't proc
            Destroy(gameObject);
        }
    }
}
