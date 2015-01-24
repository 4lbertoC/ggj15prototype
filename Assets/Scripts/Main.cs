using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour
{

		public GameObject guyPrefab;
		public GameObject housePrefab;
		private List<Vector3> positions = new List<Vector3> ();
		private Quaternion defaultRotation = Quaternion.Euler (270, 0, 0);
		private GameState gameState = GameState.GetInstance ();

		void Awake ()
		{
//				positions.Add (new Vector3 (-5, -2, 0));
//				positions.Add (new Vector3 (-3, 3, 0));
//				positions.Add (new Vector3 (5, -2, 0));
//				positions.Add (new Vector3 (3, 3, 0));
//				positions.Add (new Vector3 (0, -4, 0));
				positions.Add (new Vector3 (-3.36f, 3.98f, 0.68f));
				positions.Add (new Vector3 (2.65f, 3.78f, 2.62f));
				positions.Add (new Vector3 (-6.26f, 0.5f, -0.23f));
				positions.Add (new Vector3 (-0.61f, 0.53f, 0.98f));
				positions.Add (new Vector3 (4.62f, 2.14f, 0f));
				positions.Add (new Vector3 (-2.52f, -3.06f, -1.785f));
				positions.Add (new Vector3 (1.25f, -3.2f, -4.3f));
				positions.Add (new Vector3 (5.26f, -1.69f, -1.06f));
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
						Instantiate (housePrefab, positions [i], Quaternion.identity);
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
