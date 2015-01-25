using UnityEngine;
using System.Collections;

public class HowToPlay : MonoBehaviour {

	public GameObject howToPlayScreen;

	private bool isShow = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown() {
		howToPlayScreen.SetActive (!isShow);
		isShow = !isShow;
	}
}
