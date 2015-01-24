using UnityEngine;
using System.Collections;

public class BalloonShoot : MonoBehaviour
{

		GameState gameState = GameState.GetInstance ();

		void OnMouseDown (Guy guy)
		{
				bool isPedro = gameState.IsPedro (guy);
				if (isPedro) {
						transform.parent.gameObject.BroadcastMessage ("OnPedroShoot");	
				} else {
						transform.parent.gameObject.BroadcastMessage ("OnNotPedroShoot");	
				}
		this.transform.gameObject.SetActive (false);			
		}

}
