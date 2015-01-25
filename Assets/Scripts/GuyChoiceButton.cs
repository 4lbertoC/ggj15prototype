using UnityEngine;
using System.Collections;

public class GuyChoiceButton : MonoBehaviour
{
		public GameObject buttonShoot;
		public GameObject buttonRun;
		public ButtonRun buttonR;
		public ButtonShoot buttonS;
		private GameState gameState = GameState.GetInstance ();
		private Guy guy;
		private bool highlightActive = false;

		public void ShowChoice (Guy guy)
		{
				if (guy.GetPhase ().Equals (Guy.GuyPhase.Saved)) {
						return;
				}
				gameState.StopGame ();
				buttonS = (Instantiate (buttonShoot, 
		                        new Vector3 (3.22f, 6f, -1f), 
		                        Quaternion.identity) as GameObject).GetComponent<ButtonShoot> ();
				buttonS.gameObject.SetActive (true);
				buttonS.guy = guy;
				buttonS.guyChoiceBalloon = this;		
				buttonR = (Instantiate (buttonRun, 
		                        new Vector3 (-3.22f, 6f, -1f), 
		                        Quaternion.identity) as GameObject).GetComponent<ButtonRun> ();		
				buttonR.gameObject.SetActive (true);
				buttonR.guy = guy;
				buttonR.guyChoiceBalloon = this;
				this.guy = guy;
				SetHighlight (true);


		}

		public void RemoveButtons ()
		{
				buttonR.gameObject.SetActive (false);
				buttonS.gameObject.SetActive (false);
				SetHighlight (false);
		}

		public void SetHighlight (bool active)
		{
				highlightActive = active;
				StartCoroutine (Highlight ());
		}
	
		private IEnumerator Highlight ()
		{		
		
				while (highlightActive) {
						guy.gameObject.GetComponentInChildren<Body> ().Hide ();
						guy.gameObject.GetComponentInChildren<Body2> ().Show ();			
						yield return new WaitForSeconds (1f);
						guy.gameObject.GetComponentInChildren<Body2> ().Hide ();
						guy.gameObject.GetComponentInChildren<Body> ().Show ();
			
				}
		}

}

