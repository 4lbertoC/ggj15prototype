using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState
{
		private List<Guy> currentGuys = new List<Guy> ();
		private Guy currentPedro;
		private static GameState _instance;
		public GamePhase phase = GamePhase.Intro;	
		public enum GamePhase{Intro, Ready, TargetsChanging, Outro}

		public void Init (List<Guy> guys)
		{
				currentGuys = guys;
				currentPedro = Game.FindPedro (currentGuys);
				phase = GamePhase.Ready;				
		}
		
		public bool IsReady ()
		{
			return (phase == GamePhase.Ready);
		}

		public void EndGame (bool victory)
		{
			phase = GamePhase.Outro;
			if (victory) {
				Debug.Log ("IMPLEMENTA LA VITTORIA!!");				
			} else {
				Debug.Log ("IMPLEMENTA LA SCONFITTA!! GAME OVER, PIRLA");
				Game.LossSequence (currentPedro, currentGuys);
			}
		}

		public bool IsPedro (Guy guy)
		{
				return guy.Equals (currentPedro);
		}
		
		public Guy GetPedro() {
				return currentPedro;
		}

		public void RemoveGuy (Guy guy)
		{
				currentGuys.Remove (guy);
		}

		public static GameState GetInstance ()
		{

				if (_instance == null) {
						_instance = new GameState ();
				}

				return _instance;
		}
	
		public void ResetGame ()
		{
				phase = GamePhase.TargetsChanging;
				currentPedro = Game.FindPedro (currentGuys);
				Debug.Log ("Reset Gaming - New guys: " + currentGuys.Count);
				phase = GamePhase.Ready;
		}

		public int GetGuysCount ()
		{
				return currentGuys.Count;
		}
}

