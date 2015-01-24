using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour
{
		public GUIText timerText;
		private float deadline;
		private bool isEnded = false;
		private GameState gameState = GameState.GetInstance ();

		void Awake ()
		{
				
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (deadline <= 0 && !isEnded) {
						isEnded = true;
						gameState.ResetGame ();
						OnGuysUpdate ();
				} else if (deadline > 0 && gameState.phase.Equals(GameState.GamePhase.Ready)) {
						deadline -= Time.deltaTime;
						timerText.text = deadline.ToString ("F2");
				}
		}

		public void OnGuysUpdate ()
		{
				int guysCount = gameState.GetGuysCount ();
				deadline = guysCount * 1.5f;
				isEnded = false;
		}

}

