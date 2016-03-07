using UnityEngine;
using System.Collections;
using System;

public class Cactus : MonoBehaviour {

    public int firstFlowerLevel;

	// Use this for initialization
	void Start () {
        int diff = (Main.gameLevel - firstFlowerLevel);
        if (diff >= 0)
        {
            StartCoroutine(ShowFlower(1));
        }
        if (diff >= 1)
        {
            StartCoroutine(ShowFlower(2));
        }
        if (diff >= 2)
        {
            StartCoroutine(ShowFlower(3));
        }
    }

    private IEnumerator ShowFlower(int flower)
    {
        yield return new WaitForSeconds((firstFlowerLevel + flower - Main.MIN_GAME_LEVEL) / 2.0f);
        transform.Find("Flower " + flower).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update () {
	
	}
}
