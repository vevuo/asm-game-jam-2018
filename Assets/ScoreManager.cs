﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    public PlayerController player;
    public Text scoreText;


	// Use this for initialization
	void Start () {
        scoreText.text = "0 $";
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = player.moneyAmount() + " $";
	}
}
