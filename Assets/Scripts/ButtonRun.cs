using UnityEngine;
using System.Collections;

public class ButtonRun : MonoBehaviour
{
		
		private GameState gameState = GameState.GetInstance ();
		public Guy guy;
		public GuyChoiceButton guyChoiceBalloon;

		void OnMouseDown ()
		{
				guyChoiceBalloon.RemoveButtons ();
				gameState.ProceedByRunning(guy);			
		}	
}


