using UnityEngine;
using System.Collections;
using System;

public class Restarter : MonoBehaviour {

	public Main main;
	public ToggleButton credits;
	public ToggleButton howTo;

	void OnMouseDown() {
		main.Restart ();
	}

	public void Show() {
		howTo.TurnOff ();
		credits.TurnOff ();
		this.gameObject.transform.parent.localPosition = new Vector3 (0, 0, 0);
	}

	public void Hide() {
		this.gameObject.transform.parent.localPosition = new Vector3 (0, 15.7f, 0);
		GameObject.FindGameObjectWithTag ("AudioController").GetComponent<AudioPlayer> ().StopSound ("Victory");
	}

    internal void ToNextLevel()
    {
        main.PlayNextLevel();
    }
}
