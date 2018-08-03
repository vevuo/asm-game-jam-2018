﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Animator animator;
    private bool direction = false;

    private int money = 0;
    private Rigidbody2D rb2d;
    public float maxSpeed = 30.0f;
    public float accel = 10.0f;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (Mathf.Abs(moveHorizontal) > 0 || Mathf.Abs(moveVertical) > 0) {
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);

            if (movement.magnitude > 0)
            {
                if (rb2d.velocity.magnitude <= maxSpeed)
                    rb2d.AddRelativeForce(movement * accel);
            }
        }

        animator.SetFloat("speed", rb2d.velocity.magnitude);
        animator.SetBool("direction", rb2d.velocity.x > 0);

        if((direction && rb2d.velocity.x > 0) ||
           (!direction && rb2d.velocity.x < 0)){
            Vector3 v = transform.localScale;
            v.x *= -1;
            transform.localScale = v;
            direction = !direction;
        }
    }

    void Update () {
        
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "SunbatherTrigger")
        {
            Debug.Log("it's the trigger!");
        }
    }
}