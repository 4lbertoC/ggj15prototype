using UnityEngine;
using System.Collections;

public class Body : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Hide() {
		gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
	}
	
	public void Show() {
		gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
	}
}
