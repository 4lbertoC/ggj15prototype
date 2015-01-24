using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState
{
	private List<Guy> currentGuys = new List<Guy> ();

	private Guy currentPedro;

	private static GameState _instance;

	public void Init(List<Guy> guys) {
		currentGuys = guys;
		currentPedro = Game.FindPedro(currentGuys);
	}

	public bool isPredro(Guy guy) {
		return guy.Equals (currentPedro);
	}

	public void RemoveGuy(Guy guy){
		currentGuys.Remove(guy)
	}

	public static GameState GetInstance() {

		if(_instance == null) {
			_instance = new GameState();
		}

		return _instance;
	}
	
	public void ResetGame(){
		currentPedro = Game.FindPedro(currentGuys);
		Debug.Log ("Reset Gaming - New guys: " + currentGuys.Count) 
	}
}

