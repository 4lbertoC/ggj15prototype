using UnityEngine;
using System.Collections;

public class Corpse : MonoBehaviour {

	private float scale = 1.0f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		scale -= 0.02f;
		if (scale > 1.0f) {			
			this.transform.localScale = new Vector3(scale, scale, scale);
		} else {
			scale = 1.0f;			
			this.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		}
	}	
	
	public void Hide() {
		gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
	}
	
	public void Show() {
		scale = 1.3f;
		gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;		
	}
}
