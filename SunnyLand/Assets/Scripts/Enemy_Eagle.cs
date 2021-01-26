﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eagle : Enemy
{
    private Rigidbody2D rb;
    public Transform top, bottom;
    private bool faceUp = true;
    public float speed;
    private float topy, bottomy;
   /* private Collider2D coll;*/
    private Animator anim;

    // Start is called before the first frame update
    protected override void Start() 
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
       /* coll=GetComponent<Collider2D>();*/
        topy = top.position.y;
        bottomy = bottom.position.y;
        Destroy(top.gameObject);
        Destroy(bottom.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (faceUp)
        {
            rb.velocity = new Vector2(rb.velocity.x, speed);
            if (transform.position.y > topy)
            {
                faceUp = false;
            }
        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -speed);
            if (transform.position.y <bottomy)
            {
                faceUp = true;
            }
        }
    }
   
}