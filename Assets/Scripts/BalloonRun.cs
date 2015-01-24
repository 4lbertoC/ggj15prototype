using UnityEngine;
using System.Collections;

public class BalloonRun : MonoBehaviour
{
		
		GameState gameState = GameState.GetInstance ();
		
		void OnMouseDown (Guy guy)
		{
				bool isPedro = gameState.IsPedro (guy);
				if (isPedro) {
						transform.parent.gameObject.BroadcastMessage ("OnPedroRun");	
				} else {
						transform.parent.gameObject.BroadcastMessage ("OnNotPedroRun");	
				}
		this.transform.gameObject.SetActive (false);
			
		}
}


