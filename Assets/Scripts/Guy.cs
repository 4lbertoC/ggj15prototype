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
		private List<Arm> arms = new List<Arm> ();
		private GameObject balloon;
		private int framesBeforeShuttingUp = -1;
		private bool isSaved = false;
		private int goingAwayTime = 0;
		private bool isGone = false;
		private Vector3 hiddenScale = new Vector3 (0, 1, 0);

		void Awake ()
		{
				guyId = guyIdCumulative++;
				Debug.Log ("Guy #" + guyId + " was awaken");
		}

		// Use this for initialization
		void Start ()
		{
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (framesBeforeShuttingUp > 0) {
						framesBeforeShuttingUp--;
				} else if (framesBeforeShuttingUp == 0) {
						ShutUp ();
				}

				if (goingAwayTime > 0) {
						transform.localScale = (Vector3.Lerp (transform.localScale, hiddenScale, 1));
				}
		}

		private void SpawnArmsIfNecessary ()
		{
				if (arms.Count == 0) {
						for (int armIndex = 0; armIndex < Game.ARMS_COUNT; armIndex++) {			
								GameObject arm = Instantiate (armPrefab, this.GetPosition (), Quaternion.identity) as GameObject;
								arm.transform.parent = this.transform;						
								// arm.transform.rotation = Quaternion.LookRotation (targetGuy.GetPosition () - this.GetPosition ());
								arms.Add (arm.GetComponent<Arm> ());
								Debug.Log ("Arm #" + armIndex + " of guy #" + GetId () + " spawned");
						}
				}
		}

		private void RemoveFromScene ()
		{
//				isSaved = true;
				
				Destroy (this.gameObject);
		}
		
		void OnMouseDown ()
		{
				if (isSaved) {
						return;
				}
				if (gameState.IsReady ()) {
						bool isPedro = gameState.IsPedro (this);
						Debug.Log ("Clicked " + guyId + ". " + (isPedro ? "- he's Pedro!" : ""));
						if (isPedro) {
								gameState.RemoveGuy (this);
								ShutUp ();
								RemoveFromScene ();
								gameState.ResetGame ();
								transform.parent.gameObject.BroadcastMessage ("OnGuysUpdate");
						} else {
								Debug.Log ("Massacre - New game");
								gameState.EndGame (false);
						}
				}
			
		}
		
		private Arm GetArm (int armIndex)
		{
				SpawnArmsIfNecessary ();
				return arms [armIndex];
		}
		
		public void AimAt (int armIndex, Guy targetGuy)
		{
				Debug.Log ("Arm #" + armIndex + " of guy #" + GetId () + " aiming @ " + targetGuy.GetId ());
				Arm arm = GetArm (armIndex);
				if (arm == null) {
						Debug.Log ("No arm!");
				}
				arm.target = targetGuy.transform;
				arm.Show ();
		}
		
		public void AimAtNobody (int armIndex)
		{
				Debug.Log ("Arm #" + armIndex + " of guy #" + GetId () + " aiming @ nobody");
				Arm arm = GetArm (armIndex);			
				arm.target = null;
				// arm.Hide();
		}

		public Vector3 GetPosition ()
		{
				return this.transform.position;
		}

		public int GetId ()
		{
				return guyId;
		}
		
		public void Speak (string sentence)
		{
			
				if (balloon == null) {
						balloon = Instantiate (balloonPrefab, 
	                this.GetPosition () + new Vector3 (0.1f, 1.0f, -2.0f),
               		Quaternion.identity) as GameObject;
						// balloon.transform.parent = this.transform;
				}
				TextMesh sentenceTextMesh = balloon.GetComponentInChildren<TextMesh> ();
				sentenceTextMesh.text = sentence;
				balloon.SetActive (true);
				framesBeforeShuttingUp = 90;
		}
		
		public void ShutUp ()
		{
				if (balloon != null) {
						balloon.SetActive (false);
				}
				framesBeforeShuttingUp = -1;
		}
}
