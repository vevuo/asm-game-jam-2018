﻿﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Animator animator;
    private bool direction = false;

    private int money = 0;
    private bool carryingDrink = false;
    private string triggeringArea;
    private Rigidbody2D rb2d;
    private GameObject parent;
    public float maxSpeed = 30.0f;
    public float accel = 10.0f;

    public Transform drink;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        drink.gameObject.SetActive(carryingDrink);
    }

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (Mathf.Abs(moveHorizontal) > 0 || Mathf.Abs(moveVertical) > 0) {
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);

            if (movement.magnitude > 0)
            {
                if (rb2d.velocity.magnitude <= maxSpeed)
                    rb2d.AddRelativeForce(movement.normalized * accel);
            }
        }

        animator.SetFloat("speed", rb2d.velocity.magnitude);
        if (rb2d.velocity.magnitude > 0)
        {
            animator.SetBool("direction", moveHorizontal > 0);
        }

        if((direction && rb2d.velocity.x > 0) ||
           (!direction && rb2d.velocity.x < 0)){
            Vector3 v = transform.localScale;
            v.x *= -1;
            transform.localScale = v;
            direction = !direction;
        }
    }

    void Update () {
        if (triggeringArea != "") {
            if (Input.GetKey("space"))
            {
                if (triggeringArea == "BarTrigger")
                {
                    carryingDrink = true;
                    drink.gameObject.SetActive(carryingDrink);
                }
                else if (triggeringArea == "SunbatherTrigger")
                {
                    if (carryingDrink)
                    {
                        if (parent.GetComponent<SunbathersController>().addHydration())
                        {
                            carryingDrink = false;
                            drink.gameObject.SetActive(carryingDrink);
                        }
                    }
                    else
                    {
                        parent.GetComponent<SunbathersController>().addLotion(10.0f * Time.deltaTime);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        parent = collision.gameObject.transform.parent.gameObject;
        if (collision.gameObject.tag == "BarTrigger")
        {
            triggeringArea = "BarTrigger";
        }
        else if (collision.gameObject.tag == "SunbatherTrigger")
        {
            triggeringArea = "SunbatherTrigger";
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        triggeringArea = "";
    }
}
