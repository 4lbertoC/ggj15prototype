using UnityEngine;
using System.Collections;

public class ButtonShoot : MonoBehaviour
{

		private GameState gameState = GameState.GetInstance ();
		public Guy guy;
		public GuyChoiceButton guyChoiceBalloon;

		void OnMouseDown ()
		{
				bool isPedro = gameState.IsPedro (guy);
				if (isPedro) {
						Debug.Log ("Clicked OnPedroShoot");				

						guy.transform.parent.gameObject.BroadcastMessage ("OnPedroShoot");	
				} else {
						Debug.Log ("Clicked OnNonPedroShoot");				

						guy.transform.parent.gameObject.BroadcastMessage ("OnNonPedroShoot");	
				}
				guyChoiceBalloon.RemoveButtons ();			
		}

}
