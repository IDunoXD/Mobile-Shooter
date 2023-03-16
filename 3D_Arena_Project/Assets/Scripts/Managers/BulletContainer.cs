using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContainer : MonoBehaviour
{
    public static Transform bulletContainer;
    void Awake()
    {
        bulletContainer = transform;
    }
}
