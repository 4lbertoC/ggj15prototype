using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BackgroundManager : MonoBehaviour
{
		public SpriteRenderer spriteRenderer;
		private GameState gameState = GameState.GetInstance ();
		private float nextTransition = 0;
		private Quaternion from;
		private Quaternion to;

		void Awake ()
		{
				gameState.OnNewStandoff += NextPhase;
				from = this.gameObject.transform.rotation;
		}

		public void NextPhase ()
		{
				from = this.gameObject.transform.rotation;
				to = from * Quaternion.Euler (0, 90, 0);
				nextTransition = 0.5f;
		}

		void Update ()
		{
				if (nextTransition > 0) {
						nextTransition -= Time.deltaTime;
						transform.rotation = Quaternion.Lerp (from, to, (0.2f - nextTransition) / 0.2f);
				}
		}
}
