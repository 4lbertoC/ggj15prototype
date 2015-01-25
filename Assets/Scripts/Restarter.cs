using UnityEngine;
using System.Collections;

public class Restarter : MonoBehaviour {

	public Main main;

	void OnMouseDown() {
		main.Restart ();
	}

	public void Show() {
		this.gameObject.transform.localPosition = new Vector3 (0, 0, 0);
	}

	public void Hide() {
		this.gameObject.transform.localPosition = new Vector3 (0, 15.7f, 0);
	}
}
