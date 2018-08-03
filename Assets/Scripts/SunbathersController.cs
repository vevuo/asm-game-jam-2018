using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunbathersController : MonoBehaviour {
    private float sunburnTime = 0f;
    private float hydration = 100.0f;
    public GameObject sunbather;
    public GameObject drinkbubble;

    // Use this for initialization
    void Start()
    {
        sunburnTime = GetRandom();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isActiveAndEnabled) {
            if (sunburnTime > 0.0f)
            {
                sunburnTime -= Time.deltaTime * 3.0f;
            }
            else
            {
                sunbather.SetActive(false);
            }

            if (hydration > 0.0f)
            {
                hydration -= Time.deltaTime;
                if (hydration < 50.0f) {
                    orderDrink();
                }
            }
            else {
                sunbather.SetActive(false);
            }
        }
    }

    private float GetRandom()
    {
        return Random.Range(20.0f, 40.0f);
    }

    private void orderDrink()
    {
        drinkbubble.SetActive(true);
    }
}
