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
						SendMessage ("OnTimerEnded");
						deadline = 0;
						timerText.text = deadline.ToString ("F2");
				} else if (deadline > 0) {
						deadline -= Time.deltaTime;
						timerText.text = deadline.ToString ("F2");
				}
		}

		public void OnGuysUpdate ()
		{
				int guysCount = gameState.GetGuysCount ();
				deadline = guysCount * 10;
				isEnded = false;
		}

}

