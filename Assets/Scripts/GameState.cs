using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState
{
		public delegate void NewStandoffAction ();

		public event NewStandoffAction OnNewStandoff;

		public delegate void VictoryAction ();
		// public event VictoryAction OnVictory;

		private List<Guy> currentGuys = new List<Guy> ();
		private Guy currentPedro;
		private static GameState _instance;
		public GamePhase phase = GamePhase.Intro;	
		public enum GamePhase
		{
				Intro,
				Ready,
				TargetsChanging,
				GuyEscaping,
				Outro,
				Stopped
		}
		public GameOverType gameOverType = GameOverType.NotYetDefined;
		public enum GameOverType
		{
				NotYetDefined,
				HappyEnding,
				BittersweetEnding,
				Martyrdom,
				WarDoesntWork
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

		public void EndByShooting (Guy shooter)
		{		
				if (IsReady ()) {
						phase = GamePhase.Outro;
						if (shooter == currentPedro) {
								gameOverType = GameOverType.BittersweetEnding;
						} else {
								gameOverType = GameOverType.WarDoesntWork;
						}
						Game.ShootingSpree (shooter, shooter);
				}
		}
		
		public void ProceedByRunning (Guy runner)
		{		
				
				if (runner == currentPedro) {							
						phase = GamePhase.GuyEscaping;
						RemoveGuy (runner);
						Game.SalvationFor (runner);
						if (currentGuys.Count == 2) {					
								phase = GamePhase.Outro;
								gameOverType = GameOverType.HappyEnding;
								Game.VictorySequence (currentGuys, savedGuys);
						} else {
								ResetGame ();
						}
				} else {
						phase = GamePhase.Outro;
						gameOverType = GameOverType.Martyrdom;
						foreach (Guy guy in currentGuys) {
								if (guy.IsAimingAt (runner)) {
										Game.ShootingSpree (runner, guy);
										break;
								}
						}
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
				List<Guy> survivors = new List<Guy> ();
				foreach (Guy guy in currentGuys) {
						if (guy.IsAlive ()) {
								survivors.Add (guy);
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
				currentPedro.transform.parent.gameObject.BroadcastMessage ("OnGuysUpdate");
				OnNewStandoff ();
				Debug.Log ("Reset Gaming - New guys: " + currentGuys.Count);
				phase = GamePhase.Ready;
		}

		public int GetGuysCount ()
		{
				return currentGuys.Count;
		}

		public void MakeGuysSpeak ()
		{
				foreach (Guy g in currentGuys) {
						if (!g.Equals (currentPedro) && Random.value > 0.5f) {
								g.RandomSpeak ();
						}
				}
		}

		public void Clear ()
		{
				if (currentPedro != null) {
						currentPedro.Destroy ();
				}
				foreach (Guy g in currentGuys) {
						g.Destroy ();
				}
				foreach (Guy g in savedGuys) {
						g.Destroy ();
				}
				currentPedro = null;
				currentGuys = new List<Guy> ();
				savedGuys = new List<Guy> ();
				phase = GamePhase.Intro;
		}

		public void StopGame ()
		{
				phase = GamePhase.Stopped;
		}
}

