﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SunbathersController : MonoBehaviour {
    private const float maxSunBurn = 100f;
    private const float maxSunLotion = 100f;
    private const float maxHydration = 100f;
    private const float sunLotionRelief = -0.1f;
    private const float sunburnRateIncrease = 0.05f;
    private const int numberOfIdleAnims = 4;

    private bool alive = true;
    private float deadness = 0;

    private bool isInShade = false;
    private float amountOfSunLotion = 0f;
    private float sunburn = 0f;
    private float sunburnRate = 0.2f;

    private float dehydrationRate = 1f;
    private float hydration = 0f;
    private bool thirsty = false;

    public GameObject sunbather;
    public GameObject drinkbubble;

    private float holdPoseTime = 0f;
    private Animator animator;
    private Anima2D.SpriteMeshInstance[] sr;
    private Color redSkin = new Color(1, 0.4f, 0.4f);
    private Color deadSkin = new Color(0.7f, 0.8f, 1);
    private Color justBeforeDyingSkin = new Color();

    private void Awake()
    {
        sr = gameObject.GetComponentsInChildren<Anima2D.SpriteMeshInstance>();
        animator = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {
        alive = true;
        hydration = Random.Range(maxHydration*0.5f, maxHydration*0.6f);
        animator.SetInteger("idleAnim", (int)(Random.value * numberOfIdleAnims));
        animator.SetBool("alive", true);
        holdPoseTime = Random.Range(5f, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        if (alive)
        {
            // Apply sunburn
            if (isInShade)
            {

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

            // Dehydrate
            if (isInShade){
                // dehydration is slower in shade
            }
            else {
                hydration -= Time.deltaTime * dehydrationRate;
            }

            // Order drink when thirst comes
            if (hydration < maxHydration / 2f && !thirsty)
            {
                thirsty = true;
                orderDrink();
            }

            // Change idle animation
            holdPoseTime -= Time.deltaTime;
            if (holdPoseTime < 0)
            {
                animator.SetInteger("idleAnim", (int)(Random.value * numberOfIdleAnims));
                holdPoseTime = Random.Range(1f, 10f);
            }

            // Die from sunburn
            if (sunburn > maxSunBurn || hydration < 0)
            {
                die();
            }

            // Color the skin
            for (int i = 0; i < sr.Length; i++)
            {
                sr[i].color = Color.Lerp(Color.white, redSkin, sunburn / maxSunBurn);
            }
        }
        else {
            if (deadness < 1f)
            {
                deadness += 0.4f * Time.deltaTime;
                for (int i = 0; i < sr.Length; i++)
                {
                    sr[i].color = Color.Lerp(justBeforeDyingSkin, deadSkin, deadness);
                }
            }
        }
    }

    private void orderDrink()
    {
        drinkbubble.SetActive(true);
    }

    public bool addHydration() {
        if (thirsty && alive) {
            hydration += maxHydration / 2f;
            if(hydration > maxHydration){
                hydration = maxHydration;
            }
            thirsty = false;
            drinkbubble.SetActive(false);
            return true;
        }
        return false;
    }

    public bool addLotion(float amount) {
        if (amountOfSunLotion < maxSunLotion && alive)
        {
            amountOfSunLotion += amount;
            return true;
        }
        return false;
    }

    private void die(){
        deadness = 0;
        drinkbubble.SetActive(false);
        alive = false;
        animator.SetInteger("idleAnim", -1);
        animator.SetBool("alive", false);
        animator.Play("Sunbather_dead");
        justBeforeDyingSkin = Color.Lerp(Color.white, redSkin, sunburn / maxSunBurn);
    }

    public bool isAlive(){
        return alive;
    }
}
