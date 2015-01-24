using UnityEngine;
using System.Collections;

public class GuyChoiceBalloon : MonoBehaviour
{
	GameState gameState = GameState.GetInstance();
	public GameObject balloonShoot;
	public GameObject balloonRun;

	public void ShowChoice (Guy guy)
	{
		if (guy.GetPhase().Equals(Guy.GuyPhase.Saved)) {
			return;
		}
//		GameObject shootB = Instantiate (balloonShoot, new Vector3(3.22f,6f,0f), Quaternion.identity) as GameObject;
//		GameObject shootR = Instantiate (balloonRun, new Vector3(-3.22f,6f,0f), Quaternion.identity) as GameObject;	
//		shootB.transform.parent = guy.transform;	
//		shootB.transform.parent = guy.transform;
//		shootR.transform.parent = guy.transform;	
//		shootR.transform.parent = guy.transform;	
//		shootB.SetActive (true);
//		shootR.SetActive (true);

		bool isPedro = gameState.IsPedro (guy);
		Debug.Log ("Clicked " + guy.GetId() + (isPedro ? "- he's Pedro!" : ""));				

		// Recupera l'esito
		bool isChosenRun = true; // TEMPORANEO
		// Caso RUN:
		if (isChosenRun) {
			if (isPedro) {
					guy.PedroRun ();
			} else {
					guy.NonPedroRun();
			}
		} else {		
			// Caso SHOOT:
			if (isPedro) {
				guy.PedroRun ();
			} else {
				guy.NonPedroRun();
			}
		}
	}
}

