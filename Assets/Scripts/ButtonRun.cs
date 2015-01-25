using UnityEngine;
using System.Collections;

public class ButtonRun : MonoBehaviour
{
		
		private GameState gameState = GameState.GetInstance ();
		public Guy guy;
		public GuyChoiceButton guyChoiceBalloon;

		void OnMouseDown ()
		{

				bool isPedro = gameState.IsPedro (guy);
				if (isPedro) {
						Debug.Log ("Clicked OnPedroRun ---------------------");	
						guy.transform.parent.gameObject.BroadcastMessage ("OnPedroRun");	
				} else {
						Debug.Log ("Clicked OnNotPedroRun ---------------------");	
						guy.transform.parent.gameObject.BroadcastMessage ("OnNonPedroRun");	
				}
				guyChoiceBalloon.RemoveButtons ();
			
		}
	
}


