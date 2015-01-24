using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour
{

		public GameObject guyPrefab;
		private List<Vector3> positions = new List<Vector3> ();
		private Quaternion defaultRotation = Quaternion.Euler (270, 0, 0);
		private GameState gameState = GameState.GetInstance ();

		void Awake ()
		{
				positions.Add (new Vector3 (-5, -2, 0));
				positions.Add (new Vector3 (-3, 3, 0));
				positions.Add (new Vector3 (5, -2, 0));
				positions.Add (new Vector3 (3, 3, 0));
				positions.Add (new Vector3 (0, -4, 0));
		}

		private void PrintCombinations (Dictionary<Guy, List<Guy>> allTargets)
		{
				foreach (KeyValuePair<Guy, List<Guy>> p in allTargets) {
						int guyId = ((Guy)p.Key).GetId ();
						string others = "";
						foreach (Guy g in p.Value) {
								others += g.GetId () + ", ";
						}
	
						Debug.Log (guyId + " aims at " + others);
				}
		}

		// Use this for initialization
		void Start ()
		{
				List<Guy> guys = new List<Guy> ();

				for (int i = 0; i < positions.Count; i++) {
						GameObject guyGO = (GameObject)Instantiate (guyPrefab, positions [i], defaultRotation);
						guyGO.transform.parent = this.transform;
						Guy guy = guyGO.GetComponent<Guy> ();
						guys.Add (guy);
				}

				gameState.Init (guys);
				SendMessage ("OnGuysUpdate");

		}



		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnTimerEnded ()
		{
				Debug.Log ("Timer Ended!!!");
		}
}
