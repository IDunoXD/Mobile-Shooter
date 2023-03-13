using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] LayerMask ignoreLayer;
    [SerializeField] float gravity;
    [SerializeField] float damage;
    [SerializeField] int modifier;
    const float GuaranteedBouceAtHelatPercentage = 0.5f;
    public float OwnerHelthPercentage = 0;
    Rigidbody rb;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate(){
        rb.AddForce(Vector3.down * gravity);
    }
    void OnCollisionEnter(Collision other){
        //Ignore collisions with layer (Shooter)
        if(ignoreLayer != (ignoreLayer | (1 << other.gameObject.layer))){
            if(other.gameObject.TryGetComponent(out Health health)){
                health.GetDamage(damage, modifier);
                //Bounce if (Shooter) on low HP
                if(Random.Range(0, OwnerHelthPercentage) < GuaranteedBouceAtHelatPercentage){
                    //Bullet gain modifier after bounce
                    modifier = 1;
                    return;
                }
            }
            //Destroy if hitting something else, or bounce didn't proc
            Destroy(gameObject);
        }
    }
}
