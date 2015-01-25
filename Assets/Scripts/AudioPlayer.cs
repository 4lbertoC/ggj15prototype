using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AudioPlayer : MonoBehaviour
{

		public List<AudioSource> victorySounds;
		public List<AudioSource> caricatoreSounds;
		public List<AudioSource> escapeSounds;
		public List<AudioSource> deadSounds;
		public List<AudioSource> gunSounds;
		private Dictionary<string, List<AudioSource>> audioSources = new Dictionary<string, List<AudioSource>> ();

		void Awake ()
		{
				audioSources.Add ("Victory", victorySounds);
				audioSources.Add ("Caricatore", caricatoreSounds);
				audioSources.Add ("Escape", escapeSounds);
				audioSources.Add ("Dead", deadSounds);
				audioSources.Add ("Gun", gunSounds);
		}
	
		public void PlaySound (string id)
		{
				int randSoundIdx = (int)Mathf.Floor (Random.value * audioSources [id].Count);
				audioSources [id] [randSoundIdx].Play ();
		}

		public void StopSound (string id)
		{
				foreach (AudioSource a in audioSources[id]) {
						a.Stop ();
				}
		}
}
