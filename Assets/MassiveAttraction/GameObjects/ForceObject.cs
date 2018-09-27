using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ForceObject : MonoBehaviourBaseModuleAccessObject
{
    public Rigidbody2D rb;

    public float damage;



    public virtual void Setup()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public virtual void Reset()
    {

    }
}