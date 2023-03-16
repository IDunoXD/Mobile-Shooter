using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCollisions : MonoBehaviour
{
    [SerializeField] Red red;
    void OnTriggerEnter(Collider other){
        red.OnTrigger(other);
    }
}
