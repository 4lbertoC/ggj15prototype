using UnityEngine;
using System.Collections;

public class GuyChoiceButton : MonoBehaviour
{
		GameState gameState = GameState.GetInstance ();
		public GameObject buttonShoot;
		public GameObject buttonRun;
		public ButtonRun buttonR;
		public ButtonShoot buttonS;

		public void ShowChoice (Guy guy)
		{
				if (guy.GetPhase ().Equals (Guy.GuyPhase.Saved)) {
						return;
				}
				buttonS = (Instantiate (buttonShoot, new Vector3 (3.22f, 6f, 0f), Quaternion.identity) as GameObject).GetComponent<ButtonShoot> ();
				buttonS.gameObject.SetActive (true);
				buttonS.guy = guy;
				buttonS.guyChoiceBalloon = this;		
				buttonR = (Instantiate (buttonRun, new Vector3 (-3.22f, 6f, 0f), Quaternion.identity) as GameObject).GetComponent<ButtonRun> ();		
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

