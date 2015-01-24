using UnityEngine;
using System.Collections;

public class GuyChoiceBalloon : MonoBehaviour
{
	GameState gameState = GameState.GetInstance();

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void ShowChoice (Guy guy)
	{
		bool isPedro = gameState.IsPedro (guy);
		Debug.Log ("Clicked " + guy.GetId() + (isPedro ? "- he's Pedro!" : ""));
		if (guy.GetPhase().Equals(Guy.GuyPhase.Saved)) {
			return;
		}
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
}

