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

		if (isPedro) {
			gameState.RemoveGuy (guy);
			guy.ShutUp ();
			guy.RemoveFromScene ();
			gameState.ResetGame ();
			guy.transform.parent.gameObject.BroadcastMessage ("OnGuysUpdate");
		} else {
			Debug.Log ("Massacre - New game");
			gameState.EndGame (false);
		}

	}

	public void OnPedroShoot(Guy guy){
		gameState.RemoveGuy (guy);
		guy.ShutUp ();
		guy.RemoveFromScene ();
		gameState.ResetGame ();
		guy.transform.parent.gameObject.BroadcastMessage ("OnGuysUpdate");
	}

	public void OnNotPedroShoot(Guy guy){
		Debug.Log ("TODO - OnNotPedroShoot");
	}

	public void OnPedroRun(Guy guy){
		Debug.Log ("TODO - OnPedroRun");
	}

	public void OnNotPedroRun(Guy guy){
		Debug.Log ("TODO - OnNotPedroRun");
	}
}

