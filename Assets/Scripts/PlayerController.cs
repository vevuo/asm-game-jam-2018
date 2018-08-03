using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private int money = 0;
    private bool carryingDrink = false;
    private string triggeringArea;
    private Rigidbody2D rb2d;
    private GameObject parent;
    public float maxSpeed = 30.0f;
    public float accel = 10.0f;

    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate() {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        if (moveHorizontal != 0.0f || moveVertical != 0.0f) {
            Vector2 movement = new Vector2(moveHorizontal, moveVertical);

            if (movement.magnitude > 0)
            {
                if (rb2d.velocity.magnitude <= maxSpeed)
                    rb2d.AddRelativeForce(movement * accel);
            }
        }
    }

    void Update () {
        if (triggeringArea != "") {
            if (Input.GetKey("space"))
            {
                if (triggeringArea == "BarTrigger")
                {
                    carryingDrink = true;
                }
                else if (triggeringArea == "SunbatherTrigger")
                {
                    if (carryingDrink)
                    {
                        parent.GetComponent<SunbathersController>().addHydration();
                        carryingDrink = false;
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
