using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioPlayer : MonoBehaviour {

	public AudioSource victorySound;

	private Dictionary<string, AudioSource> audioSources = new Dictionary<string, AudioSource>();

	void Awake() {
		audioSources.Add ("Victory", victorySound);
	}

	public void PlaySound(string id) {
		audioSources [id].Play ();
	}

	public void StopSound(string id) {
		audioSources [id].Stop ();
	}
}
