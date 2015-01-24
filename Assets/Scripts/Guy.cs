using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Guy : MonoBehaviour
{

		public GameObject armPrefab;
		public GameObject balloonPrefab;
		private static int guyIdCumulative = 0;
		private int guyId;
		private GameState gameState = GameState.GetInstance ();
		private List<GameObject> arms = new List<GameObject> ();

		void Awake ()
		{
				guyId = guyIdCumulative++;
		}

		// Use this for initialization
		void Start ()
		{
		}
	
		// Update is called once per frame
		void Update ()
		{
			
		}

		void OnMouseDown ()
		{
				bool isPedro = gameState.isPedro (this);
				Debug.Log ("Clicked " + guyId + ". " + (isPedro ? "Is Pedro!" : ""));
				if (isPedro) {
						gameState.RemoveGuy (this);
						Destroy(this.gameObject);
						gameState.ResetGame ();
				} else {
						Debug.Log ("Dead - New Game");
				}
			
		}

		public void AimAt (Guy targetGuy)
		{
				GameObject arm = Instantiate (armPrefab, this.GetPosition (), Quaternion.identity) as GameObject;
				arm.transform.parent = this.transform;
				arm.transform.rotation = Quaternion.LookRotation (targetGuy.GetPosition () - this.GetPosition ());
				arms.Add (arm);
		}

		public Vector3 GetPosition ()
		{
				return this.transform.position;
		}

		public int GetId ()
		{
				return guyId;
		}

		public void ResetArms ()
		{
				foreach (GameObject arm in arms) {
						Destroy (arm);
				}
			
				arms.Clear ();
		}
		
		public void Speak(string sentence) {
			
			GameObject balloon = Instantiate (balloonPrefab, 
                this.GetPosition () + new Vector3(0.1f, 1.0f, -2.0f),
				Quaternion.identity) as GameObject;
			TextMesh sentenceTextMesh = balloon.GetComponentInChildren<TextMesh>();
			sentenceTextMesh.text = sentence;
			balloon.SetActive(true);
		}
}
