using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game
{

		public const int ARMS_COUNT = 2;
		private const float LIKELIHOOD_OF_MORE_THAN_ONE_ARM = 0.2f;

		private static List<Guy> ChooseTargets (Guy shootingGuy, PriorityQueue otherGuys)
		{
				List<Guy> targets = new List<Guy> ();
				for (int armIndex = 0; armIndex < ARMS_COUNT; armIndex++) {
						if (armIndex == 0 || Random.value < LIKELIHOOD_OF_MORE_THAN_ONE_ARM) {
								Guy targetGuy = otherGuys.GetAndTouchHeadButNot (shootingGuy) as Guy;
								targets.Add (targetGuy);
								shootingGuy.AimAt (armIndex, targetGuy);
						} else {
								shootingGuy.AimAtNobody (armIndex);
						}
				}
				return targets;
		}

		public static Guy FindPedro (List<Guy> guys)
		{		

				Dictionary<Guy, List<Guy>> allTargets = new Dictionary<Guy, List<Guy>> ();
				List<Guy> nonPedroes = new List<Guy> (guys);
				GameState gameState = GameState.GetInstance ();
				PrintList ("All", nonPedroes);
	
				int pedroIndex = (int)Mathf.Floor (Random.value * guys.Count);
				Guy pedro = nonPedroes [pedroIndex];
				pedro.Speak ("soy Pedro");
				Debug.Log (pedro.GetId () + " is Pedro!");
				nonPedroes.Remove (pedro);
				PrintList ("All without Pedro", nonPedroes);
				
				List<Guy> shuffledNonPedroes = new List<Guy> ();
				while (nonPedroes.Count > 0) {
						
						int index = (int)Mathf.Floor (Random.value * nonPedroes.Count);
						shuffledNonPedroes.Add (nonPedroes [index]);
						nonPedroes.RemoveAt (index);
				}
				
				PrintList ("All without Pedro shuffled", shuffledNonPedroes);
				PriorityQueue targetsQueue = new PriorityQueue (shuffledNonPedroes);		
				allTargets.Add (pedro, ChooseTargets (pedro, targetsQueue));
				foreach (Guy nonPedro in shuffledNonPedroes) {
						allTargets.Add (nonPedro, ChooseTargets (nonPedro, targetsQueue));
						if (gameState.IsIntro ()) {
								nonPedro.Speak ();
						}
				}
				return pedro;
		}
		
		public static void LossSequence (Guy pedro, List<Guy> nonPedroes)
		{
				pedro.ShootSlow ();
		}
		
		public static void LossSequenceCoupDeGrace ()
		{
				GameState gameState = GameState.GetInstance ();
				if (gameState.IsOutro ()) {
						List<Guy> survivors = gameState.GetSurvivorsWithGuns ();
						if (survivors.Count > 1) {
								Debug.Log ("Coup de grace needed, " + survivors.Count + " survivors.");
								foreach (Guy guy in survivors) {
										guy.ShootFast ();
								}
						} else {
								Debug.Log ("Coup de grace not needed, one survivor.");
						}
				}
		}
		
		private static void PrintList (string message, List<Guy> guysToPrint)
		{
				string output = message + ": ";
				foreach (Guy guy in guysToPrint) {								
						output += guy.GetId () + " ";
				}
				Debug.Log (output);
		}

		public static void VictorySequence (Guy pedro, List<Guy> nonPedroes, List<Guy> savedGuys)
		{
		Debug.Log ("victory!!!");
				Guy otherSurvivor = null;
				foreach (Guy g in nonPedroes) {
						if (!savedGuys.Contains (g) && !g.Equals(pedro)) {
								otherSurvivor = g;
								break;
						}
				}

				pedro.ShowAndNowMessage (otherSurvivor, savedGuys);
		}
}
