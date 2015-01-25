using UnityEngine;
using System.Collections;

public class GuyChoiceButton : MonoBehaviour
{
		public GameObject buttonShoot;
		public GameObject buttonRun;
		public ButtonRun buttonR;
		public ButtonShoot buttonS;
		private GameState gameState = GameState.GetInstance ();

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

		}

		public void RemoveButtons ()
		{
				buttonR.gameObject.SetActive (false);
				buttonS.gameObject.SetActive (false);
		}

	
}

