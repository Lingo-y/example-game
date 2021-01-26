using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Frog : Enemy
{
    private Rigidbody2D rb;
    public Transform leftpoint, rightpoint;
    private bool faceLeft = true;
    public float speed,jumpForce;
    private float leftx, rightx;
    private Collider2D coll;
   /* private Animator anim;*/
    public LayerMask Ground;
    // Start is called before the first frame update
    
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        /*anim = GetComponent<Animator>();*/
        coll = GetComponent<Collider2D>();
        transform.DetachChildren();
        leftx = leftpoint.position.x;
        rightx = rightpoint.position.x;
        Destroy(leftpoint.gameObject);
        Destroy(rightpoint.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        SwitchAnim();
    }
    void Movement()
    {
        if (faceLeft)
        {
            if (coll.IsTouchingLayers(Ground))
            {
                anim.SetBool("Jumping", true);
                rb.velocity = new Vector2(-speed, jumpForce);
            }
            if (transform.position.x < leftx)
            {
                transform.localScale = new Vector3(-1, 1, 1);
                faceLeft = false;
            }
        }
        else
        {
            if (coll.IsTouchingLayers(Ground))
            {
                anim.SetBool("Jumping", true);
                rb.velocity = new Vector2(speed, jumpForce);
            }
            if (transform.position.x > rightx)
            {
                transform.localScale = new Vector3(1, 1, 1);
                faceLeft = true;

            }
        }
    }
    void SwitchAnim()
    {
        if (anim.GetBool("Jumping"))
        {
            if (rb.velocity.y < 0.1)
            {
                anim.SetBool("Jumping", false);
                anim.SetBool("Falling", true);
            }
        }
        if (coll.IsTouchingLayers(Ground))
        {
            anim.SetBool("Falling", false);
        }
    }
   
}
