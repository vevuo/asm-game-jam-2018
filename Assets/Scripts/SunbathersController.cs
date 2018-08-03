using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunbathersController : MonoBehaviour {
    private const float maxSunBurn = 100f;
    private const float maxSunLotion = 100f;
    private const float sunLotionRelief = -5f;
    private const float sunburnRateIncrease = 0.1f;

    private bool isInShade = false;
    private float amountOfSunLotion = 0f;
    private float sunburn = 0f;
    private float sunburnRate = 1f;

    private float sunburnTime = 0f;
    private float hydration = 0f;
    private bool thirsty = false;

    public GameObject sunbather;
    public GameObject drinkbubble;

    private Anima2D.SpriteMeshInstance[] sr;
    //private SpriteRenderer sr;
    private Color redSkin = new Color(1, 0, 0);

    private void Awake()
    {
        //sr = GetComponent<SpriteRenderer>();
        sr = gameObject.GetComponentsInChildren<Anima2D.SpriteMeshInstance>();
    }

    // Use this for initialization
    void Start()
    {
        sunburnTime = Random.Range(70.0f, 120.0f);
        hydration = Random.Range(40.0f, 60.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isInShade) {

        }
        else if (amountOfSunLotion > 0)
        {
            sunburn += sunLotionRelief * Time.deltaTime;
            amountOfSunLotion -= Time.deltaTime;
        }
        else
        {
            sunburn += sunburnRate * Time.deltaTime;
            sunburnRate += sunburnRateIncrease * Time.deltaTime;
            if (sunburn < 0)
            {
                sunburn = 0;
            }
        }

        for (int i = 0; i < sr.Length; i++) {
            sr[i].color = Color.Lerp(Color.white, redSkin, sunburn / maxSunBurn);
        }

        if (this.isActiveAndEnabled) {
            if (sunburnTime > 0.0f && hydration > 0.0f)
            {
                sunburnTime -= Time.deltaTime * 3.0f;
                hydration -= Time.deltaTime * 2.25f;
                if (hydration < 20.0f) {
                    thirsty = true;
                    orderDrink();
                }
            }
            else
            {
                drinkbubble.SetActive(false);
                sunbather.SetActive(false);
            }
        }
    }

    private void orderDrink()
    {
        drinkbubble.SetActive(true);
    }

    public bool addHydration() {
        if (thirsty) {
            hydration += 50.0f;
            drinkbubble.SetActive(false);
            return true;
        }
        return false;
    }

    public bool addLotion(float amount) {
        amountOfSunLotion += amount;
        if(amountOfSunLotion > maxSunLotion){
            amountOfSunLotion = maxSunLotion;
            return false;
        }
        return true;
    }
}
