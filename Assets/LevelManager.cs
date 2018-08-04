using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    private GameObject[] sunbathers;
    private int currentLevel = 0;
    private float levelUpTime = 0;
    public int levelUpTimeSeconds = 30;
    public GameObject UIGameOver;
    public PlayerController player;


	// Use this for initialization
	void Start () {
        sunbathers = GameObject.FindGameObjectsWithTag("Sunbather");
        foreach(GameObject sb in sunbathers){
            sb.SetActive(false);
        }
        sunbathers[(int)(Random.value * sunbathers.Length)].SetActive(true);
        levelUpTime = 0;
        currentLevel = 0;

        UIGameOver.SetActive(false);
	}

    // Update is called once per frame
    void Update()
    {

        foreach (GameObject sb in sunbathers)
        {
            if(!sb.GetComponent<SunbathersController>().isAlive()){
                // Game over
                UIGameOver.SetActive(true);
            }
        }

        levelUpTime += Time.deltaTime;
        if (levelUpTime > levelUpTimeSeconds)
        {
            levelUpTime = 0;
            if (sunbathers.Length > currentLevel)
            {
                currentLevel++;
                GameObject tmp;
                do
                {
                    tmp = sunbathers[(int)(Random.value * sunbathers.Length)];
                } while (tmp.activeInHierarchy);
                tmp.SetActive(true);
            }
        }
    }
}
