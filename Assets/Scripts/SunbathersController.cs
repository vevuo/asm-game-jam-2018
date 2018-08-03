using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunbathersController : MonoBehaviour {
    private float sunburnTime = 0f;
    private float hydration = 0f;
    public GameObject sunbather;
    public GameObject drinkbubble;

    // Use this for initialization
    void Start()
    {
        sunburnTime = Random.Range(70.0f, 120.0f);
        hydration = Random.Range(40.0f, 60.0f);
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
                hydration -= Time.deltaTime * 2.25f;
                if (hydration < 20.0f) {
                    orderDrink();
                }
            }
            else {
                sunbather.SetActive(false);
            }
        }
    }

    private void orderDrink()
    {
        drinkbubble.SetActive(true);
    }


}
