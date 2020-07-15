using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
		public Text timerText;
		private float deadline;
		private bool isEnded = false;
		private GameState gameState = GameState.GetInstance ();

		void Awake ()
		{
				
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (!gameState.IsReady ()) {
						return;
				}
				if (deadline <= 0 && !isEnded) {
						isEnded = true;
						gameState.MakeGuysSpeak ();
						StartCoroutine (ResetTimerCoroutine ());
				} else if (deadline > 0) {
						deadline -= Time.deltaTime;
						timerText.text = deadline.ToString ("F2");
				}
		}

		public void OnGuysUpdate ()
		{
				ResetTimer ();
		}

		private void ResetTimer ()
		{
				int guysCount = gameState.GetGuysCount ();
				deadline = guysCount * 1.0f;
				isEnded = false;
		}

		IEnumerator ResetTimerCoroutine ()
		{
				yield return new WaitForSeconds (0.5f);
				if (gameState.IsReady ()) {
						gameState.ResetGame ();
				}
				ResetTimer ();
		}

		public void StopTimer ()
		{
				isEnded = true;
		}

}

