using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ArenaEdge : MonoBehaviour
{
    [SerializeField] LayerMask AcceptableCollisionLayer;
    void OnCollisionEnter(Collision other){
        if(AcceptableCollisionLayer == (AcceptableCollisionLayer | (1 << other.gameObject.layer))){
            Debug.Log(other.gameObject.name);
        }
    }
}
