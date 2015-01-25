using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameState
{
		public delegate void NewStandoffAction ();

		public event NewStandoffAction OnNewStandoff;

		public delegate void VictoryAction ();
		// public event VictoryAction OnVictory;

		private AudioPlayer audioPlayer;
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
				SetPhase (GamePhase.Ready);
		}
		
		public bool IsReady ()
		{
				return (phase == GamePhase.Ready);
		}

		public bool IsStopped ()
		{
				return (phase == GamePhase.Stopped);
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
				if (IsStopped ()) {
						
						
						if (shooter == currentPedro) {
								SetPhase (GamePhase.Outro, GameOverType.BittersweetEnding);
						} else {
								SetPhase (GamePhase.Outro, GameOverType.WarDoesntWork);
						}
						Game.ShootingSpree (shooter, shooter);
				}
		}
		
		public void ProceedByRunning (Guy runner)
		{		
				
				if (runner == currentPedro) {							
						SetPhase (GamePhase.GuyEscaping);
						RemoveGuy (runner);
						Game.SalvationFor (runner);
						if (currentGuys.Count == 2) {					
								SetPhase (GamePhase.Outro, GameOverType.HappyEnding);
								Game.VictorySequence (currentGuys, savedGuys);
						} else {
								ResetGame ();
						}
				} else {
						SetPhase (GamePhase.Outro, GameOverType.Martyrdom);
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
				SetPhase (GamePhase.TargetsChanging);
				currentPedro = Game.FindPedro (currentGuys);
				currentPedro.transform.parent.gameObject.BroadcastMessage ("OnGuysUpdate");
				OnNewStandoff ();
				Debug.Log ("Reset Gaming - New guys: " + currentGuys.Count);
				SetPhase (GamePhase.Ready);
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
				SetPhase (GamePhase.Intro);
		}

		public bool AreBulletsFlying ()
		{
				return GameObject.FindGameObjectsWithTag ("Bullet").Length > 0;
		}

		public void StopGame ()
		{
				SetPhase (GamePhase.Stopped);
		}

		void SetPhase (GamePhase phase)
		{
				SetPhase (phase, GameOverType.NotYetDefined);
		}

		void SetPhase (GamePhase phase, GameOverType gameOverType)
		{
				GamePhase oldPhase = this.phase;
				this.phase = phase;
				this.gameOverType = gameOverType;
				if (phase != oldPhase) {
						Debug.Log ("Phase change from " + oldPhase + " to " + phase);
						ChangeMusic ();
				}
		}

		void ChangeMusic ()
		{
				if (audioPlayer != null) {
						switch (phase) {
						case GamePhase.Ready:
								audioPlayer.PlayMusic (1);
								break;
						case GamePhase.Outro:
								if (gameOverType.Equals (GameOverType.HappyEnding)) {
										audioPlayer.PlayMusic (2);
								} else {
										audioPlayer.PlayMusic (3);
								}
								break;
						}
				}
		}

		public void SetAudioPlayer (AudioPlayer player)
		{
				Debug.Log ("Player set to " + player);
				audioPlayer = player;
		}

}

