using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState
{
		public delegate void RemoveGuyAction();
		public event RemoveGuyAction OnRemoveGuy;

		public delegate void VictoryAction ();
		public event VictoryAction OnVictory;

		private List<Guy> currentGuys = new List<Guy> ();
		private Guy currentPedro;
		private static GameState _instance;
		public GamePhase phase = GamePhase.Intro;	
		public enum GamePhase
		{
				Intro,
				Ready,
				TargetsChanging,
				Outro
		}

		private List<Guy> savedGuys = new List<Guy> () ;

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

		public bool IsIntro ()
		{
				return (phase == GamePhase.Intro);
		}

		public bool IsOutro ()
		{
				return (phase == GamePhase.Outro);
		}
	
		public void EndGame (bool victory)
		{
			phase = GamePhase.Outro;
			if (victory) {
				Game.VictorySequence(currentPedro, currentGuys, savedGuys);		
			} else {
				Game.LossSequence (currentPedro, currentGuys);
			}
		}

		public bool IsPedro (Guy guy)
		{
				return guy.Equals (currentPedro);
		}
		
		public Guy GetPedro ()
		{
				return currentPedro;
		}
		
		public List<Guy> GetSurvivorsWithGuns ()
		{
			List<Guy> survivors = new List<Guy>();
			foreach (Guy guy in currentGuys) {
				if (guy.IsAlive()) {
					survivors.Add(guy);
				}
			}
			return survivors;
		}

		public int GetSavedGuysCount ()
		{
				return savedGuys.Count;
		}

		public void RemoveGuy (Guy guy)
		{
				currentGuys.Remove (guy);
				savedGuys.Add (guy);
				OnRemoveGuy ();
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

