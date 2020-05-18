using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    private BoxCollider2D boxCollider;         
    private Rigidbody2D rb2D;                

    protected virtual void Start()
    {
        
        boxCollider = GetComponent<BoxCollider2D>();

        rb2D = GetComponent<Rigidbody2D>();

    }

}