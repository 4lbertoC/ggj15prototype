using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Guy : MonoBehaviour
{
		public enum GuyPhase
		{
				Ready,
				Saved,
				Gone,
				AndNow,
				Tequila,
				Victorious
		}

		public GameObject armPrefab;
		public GameObject balloonPrefab;
		public Sprite savedSprite;
		public Sprite savedFiestaSprite;
		private static int guyIdCumulative = 0;
		private int guyId;
		private GameState gameState = GameState.GetInstance ();
		private List<Arm> arms = new List<Arm> ();
		private bool dead = false;
		private bool scared = false;
		private bool chased = false;
		public GameObject balloon;
		private int framesBeforeShuttingUp = -1;
		private float transitionTime = 0;
		private List<string> sentences = new List<string> ();
		private readonly Vector3 HIDDEN_SCALE = new Vector3 (0, 1, 0);
		private readonly Vector3 VISIBLE_SCALE = new Vector3 (1, 1, 1);
		private readonly float STARTING_TRANSITION_TIME = 0.5f;
		private readonly Vector3 SAVED_STARTING_POSITION = new Vector3 (-5.5f, 5.47f, 0);
		private readonly float SAVED_OFFSET = 0.8f;
		private const float REVENGE_TIME = 0.2f;
		private const float AGONY_TIME = 0.2f;
		private GuyPhase phase = GuyPhase.Ready;
		private SpriteRenderer spriteRenderer;
		public GuyChoiceButton guyChoiceBalloon;
		public GameObject andNowBalloon;
		public GameObject tequilaBalloon;
		private AudioPlayer audioPlayer;
		public List<Sprite> balloonSprites;
		public Sprite balloonOh;
		public Sprite balloonNoo;
		public Sprite balloonStop;

		void Awake ()
		{
				guyId = guyIdCumulative++;
				
				sentences.Add ("Hey");
				sentences.Add ("...!");
				sentences.Add ("Don't");
				sentences.Add ("WTF?");		
				sentences.Add ("Give up");	
				sentences.Add ("Back down");
				sentences.Add ("Run");
				sentences.Add ("?!");
				// Debug.Log ("Guy #" + guyId + " was awaken");
				spriteRenderer = GetComponentInChildren<SpriteRenderer> ();
				audioPlayer = GameObject.FindGameObjectWithTag ("AudioController").GetComponent<AudioPlayer> ();
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

				if (phase.Equals (GuyPhase.Saved)) {
						if (transitionTime > 0) {
								transitionTime -= Time.deltaTime;
								transform.localScale = (Vector3.Lerp (transform.localScale, HIDDEN_SCALE, (STARTING_TRANSITION_TIME - transitionTime) / STARTING_TRANSITION_TIME));
						} else {
								phase = GuyPhase.Gone;
								spriteRenderer.sprite = savedSprite;
								transitionTime = STARTING_TRANSITION_TIME;
								transform.localPosition = SAVED_STARTING_POSITION + new Vector3 (SAVED_OFFSET * gameState.GetSavedGuysCount (), 0, 0);
						}
				} else if (phase.Equals (GuyPhase.Gone)) {
						if (transitionTime > 0) {
								transitionTime -= Time.deltaTime;
								transform.localScale = (Vector3.Lerp (transform.localScale, VISIBLE_SCALE, (STARTING_TRANSITION_TIME - transitionTime) / STARTING_TRANSITION_TIME));
						}
				} else if (phase.Equals (GuyPhase.Victorious)) {
						if ((Mathf.Floor (Time.time * 5) % 2) == 0) {
								spriteRenderer.sprite = savedFiestaSprite;
						} else {
								spriteRenderer.sprite = savedSprite;
						}
				}
				
				// DEBUG KEYS: RIMUOVILEEE
//				if (!gameState.IsPedro (this) && Input.GetKeyDown (KeyCode.Z)) {
//						if (GetId () == 0) {
//								Debug.Log (GetId () + ": 0 pressed - LEVA QUESTA MERDAAAAAAAAAAAAAAAAAAAAAAA");
//						}
//						OnNonPedroShoot ();
//				}
//				
//				if (gameState.IsPedro (this) && Input.GetKeyDown (KeyCode.P)) {
//						Debug.Log (GetId () + ": P pressed - LEVA QUESTA MERDAAAAAAAAAAAAAAAAAAAAAAA");
//						OnPedroShoot ();
//				}
		}

		private void SpawnArmsIfNecessary ()
		{
				if (arms.Count == 0) {
						for (int armIndex = 0; armIndex < Game.ARMS_COUNT; armIndex++) {			
								GameObject arm = Instantiate (armPrefab, this.GetPosition (), Quaternion.identity) as GameObject;
								arm.transform.parent = this.transform;						
								// arm.transform.rotation = Quaternion.LookRotation (targetGuy.GetPosition () - this.GetPosition ());
								arms.Add (arm.GetComponent<Arm> ());
								// Debug.Log ("Arm #" + armIndex + " of guy #" + GetId () + " spawned");
						}
				}
				foreach (Arm arm in arms) {
						arm.Show ();
				}
		}

		public void RemoveFromScene ()
		{
				audioPlayer.PlaySound ("Escape");
				phase = GuyPhase.Saved;
				transitionTime = STARTING_TRANSITION_TIME;
				AimAtNobody (0);
				AimAtNobody (1);
				foreach (Arm arm in arms) {
						arm.Hide ();
				}
		}
		
		void OnMouseDown ()
		{
				Debug.Log ("Clicked on guy #" + GetId ());
				if (gameState.IsReady ()) {
						guyChoiceBalloon.ShowChoice (this);					
				}
			
		}
		
		private Arm GetArm (int armIndex)
		{
				SpawnArmsIfNecessary ();
				return arms [armIndex];
		}
		
		public void AimAt (int armIndex, Guy targetGuy)
		{
				if (!phase.Equals (GuyPhase.Ready)) {
						return;
				}
				// Debug.Log ("Arm #" + armIndex + " of guy #" + GetId () + " aiming @ " + targetGuy.GetId ());
				audioPlayer.PlaySound ("Caricatore");
				Arm arm = GetArm (armIndex);
				if (arm == null) {
						Debug.Log ("No arm!");
				}
				arm.target = targetGuy;
				arm.Show ();
		}

		public void WaitAndSuddenlyAimAt (int armIndex, Guy targetGuy)
		{
				StartCoroutine (AfterRandomPauseAimCoroutine (armIndex, targetGuy));
		}
		
		IEnumerator AfterRandomPauseAimCoroutine (int armIndex, Guy targetGuy)
		{		
				yield return new WaitForSeconds (Random.value * 3);
				if (Random.value > 0.7) {
						RandomSpeak ();
				}
				AimAt (armIndex, targetGuy);
		}
		
		public void AimAtNobody (int armIndex)
		{
				// Debug.Log ("Arm #" + armIndex + " of guy #" + GetId () + " aiming @ nobody");
				Arm arm = GetArm (armIndex);			
				arm.target = null;
		}

		public bool IsAimingAt (Guy candidateTarget)
		{
				foreach (Arm arm in arms) {
						if (arm.target == candidateTarget) {
								return true;
						}
				}
				return false;
		}

		public Vector3 GetPosition ()
		{
				return this.transform.position;
		}

		public int GetId ()
		{
				return guyId;
		}
		
		public void Speak (Sprite sentence)
		{	
				balloon.SetActive (true);
				balloon.GetComponent<SpriteRenderer> ().sprite = sentence;
				framesBeforeShuttingUp = 90;
		}
		
		public void RandomSpeak ()
		{
				balloon.SetActive (true);
				int random = Random.Range (0, balloonSprites.Count);
				// Debug.Log ("Random speak " + random);
				balloon.GetComponent<SpriteRenderer> ().sprite = balloonSprites [random];
				framesBeforeShuttingUp = 90;
		}
	
		public void ShutUp ()
		{
				balloon.SetActive (false);
				framesBeforeShuttingUp = -1;
		}

		public void Chase (Guy chasedGuy)
		{
				Speak (balloonStop);
				chasedGuy.chased = true;
		}

		void BeScared ()
		{
				if (!scared) {
						scared = true;
						if (!chased) {
								for (int armIndex = 0; armIndex < arms.Count; armIndex++) {
										AimAtNobody (armIndex);
								}	
						}
						StartCoroutine (ScreamOhNoCoroutine ());
				}
		}
		
		IEnumerator ScreamOhNoCoroutine ()
		{
				yield return new WaitForSeconds (0.5f);	
				Speak (balloonOh);
				yield return new WaitForSeconds (1.0f);	
				Speak (balloonNoo);
		}
		
		public void ShootButRememberThatGuyIsSpecial (Guy specialGuy)
		{
				Debug.Log ("Guy #" + GetId () + " about to shoot");
				foreach (Arm arm in arms) {
						if (arm.target != null) {
								audioPlayer.PlaySound ("Gun");
								if (arm.target == specialGuy) {
										arm.Shoot (2.0f, null);				
										specialGuy.BeScared ();
								} else {
										arm.ShootWithDelay (15.0f, specialGuy);
								}
						}
				}
		}

		IEnumerator DeathCoroutine (Guy specialGuy)
		{		
				ShootButRememberThatGuyIsSpecial (specialGuy);
				yield return new WaitForSeconds (AGONY_TIME);
				gameObject.GetComponentInChildren<Body> ().Hide ();
				foreach (Arm arm in arms) {
						arm.Hide ();
				}
				gameObject.GetComponentInChildren<Corpse> ().Show ();
				audioPlayer.PlaySound ("Dead");
				dead = true;
				if (scared) {			
						yield return new WaitForSeconds (3.0f);
						Game.LossSequenceCoupDeGrace ();
				}
		}

		
	
		public void Die (Guy specialGuy)
		{
				if (IsAlive ()) {			
						StartCoroutine (DeathCoroutine (specialGuy));
				}
		}
		
		public void Raise ()
		{
				if (!IsAlive ()) {
						gameObject.GetComponentInChildren<Corpse> ().Hide ();
						gameObject.GetComponentInChildren<Body> ().Show ();			
						foreach (Arm arm in arms) {
								arm.Show ();
						}
						dead = false;
				}
		}

		public bool IsAlive ()
		{
				return !dead;
		}

		public GuyPhase GetPhase ()
		{
				return phase;
		}

		public void ShowAndNowMessage (Guy otherGuy, List<Guy> savedGuys)
		{
				StartCoroutine (ShowAndNowMessageCoroutine (otherGuy, savedGuys));
		}

		IEnumerator ShowAndNowMessageCoroutine (Guy otherGuy, List<Guy> savedGuys)
		{
				yield return new WaitForSeconds (2.0f);
				andNowBalloon.SetActive (true);
				otherGuy.ShowTequilaMessage (savedGuys);
		}

		public void ShowTequilaMessage (List<Guy> savedGuys)
		{
				StartCoroutine (ShowTequilaMessageCoroutine (savedGuys));
		}

		IEnumerator ShowTequilaMessageCoroutine (List<Guy> savedGuys)
		{
				yield return new WaitForSeconds (2.0f);
				audioPlayer.PlaySound ("Victory");
				tequilaBalloon.SetActive (true);
				foreach (Guy g in savedGuys) {
						g.ShowVictorious ();
				}
				StartCoroutine (ShowPlayButtonCoroutine (2.0f, false));
		}

		public void ShowVictorious ()
		{
				phase = GuyPhase.Victorious;
		}

		IEnumerator ShowPlayButtonCoroutine (float seconds, bool checkDeath)
		{
				const float CHECK_PERIOD = 0.5f;
				while (checkDeath && !dead) {			
						yield return new WaitForSeconds (CHECK_PERIOD);
				}
				yield return new WaitForSeconds (seconds);
				// Check twice to be "sure" that the bullets are not flying or about to fly
				while (gameState.AreBulletsFlying()) {			
						yield return new WaitForSeconds (CHECK_PERIOD);
						while (gameState.AreBulletsFlying()) {
								yield return new WaitForSeconds (CHECK_PERIOD);
								Debug.Log ("Bullets are still flying... wait");
						}
				}
				Debug.Log ("Bullets did not fly in the last " + CHECK_PERIOD * 2 + " seconds");
				GameObject.FindGameObjectWithTag ("PlayButton").GetComponent<Restarter> ().Show ();
		}

		public void Destroy ()
		{
				Destroy (this.gameObject);
		}

		public void ShowPlayButtonAfterDeath ()
		{
				StartCoroutine (ShowPlayButtonCoroutine (5.0f, true));
		}
}
