using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour
{

		public GUIText timerText;
		private float deadline;
		private bool isEnded = false;

		void Awake ()
		{
				deadline = 3.0f;
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

		public void SetTimer (float time)
		{
				deadline = time;
				isEnded = false;
		}

}

