using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public Guy target;	
	public float speed;
	public Guy specialGuy;
	
	// Use this for initialization
	void Start () {
		if (target == null) {
			Debug.Log ("Bullet starting with no target");
		} else {
			Debug.Log ("Bullet starting with target=" + target.GetId ());
		}
		
		if (specialGuy != null) {
			Debug.Log ("                and special=" + specialGuy.GetId ());			
		} else {
			Debug.Log ("                and no special guy");
		}
	}
	
	// Update is called once per frame
	void Update () {
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);
		if ((transform.position - target.transform.position).magnitude < 0.1f) {
			Debug.Log ("Guy #" + target.GetId () + " gets killed");			
			target.Die(specialGuy);
			this.gameObject.SetActive(false);
		}
	}
}
