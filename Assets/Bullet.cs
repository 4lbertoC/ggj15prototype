using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public Guy target;	
	public float speed;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
		if ((transform.position - target.transform.position).magnitude < 0.1f) {
			Debug.Log ("Guy #" + target.GetId () + " gets killed");			
			target.Die();
			this.gameObject.SetActive(false);
		}
	}
}
