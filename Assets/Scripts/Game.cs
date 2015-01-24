using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game
{

		private const int MAX_TARGET_COUNT = 2;

		private static List<Guy> ChooseTargets (Guy shootingGuy, PriorityQueue otherGuys)
		{
				List<Guy> targets = new List<Guy> ();
				int targetsCount = (int)Mathf.Ceil (Random.value * MAX_TARGET_COUNT);
				while (targetsCount > 0) {
						Guy targetGuy = otherGuys.GetAndTouchHeadButNot (shootingGuy) as Guy;
						targets.Add (targetGuy);
						shootingGuy.AimAt (targetGuy);
						--targetsCount;
				}
				return targets;
		}

		public static Guy FindPedro (List<Guy> guys)
		{		
				Dictionary<Guy, List<Guy>> allTargets = new Dictionary<Guy, List<Guy>> ();
				List<Guy> nonPedroes = new List<Guy> (guys);				
				foreach (Guy g in guys) {
					g.ResetArms();
				}
				
				PrintList("All", nonPedroes);
	
				int pedroIndex = (int)Mathf.Floor (Random.value * guys.Count);
				Guy pedro = nonPedroes [pedroIndex];
				Debug.Log (pedro.GetId () + " is Pedro!");
				nonPedroes.Remove (pedro);
				PrintList("All without Pedro", nonPedroes);
				
				List<Guy> shuffledNonPedroes = new List<Guy>();
				while (nonPedroes.Count > 0) {
						
						int index = (int)Mathf.Floor (Random.value * nonPedroes.Count);
						shuffledNonPedroes.Add (nonPedroes [index]);
						nonPedroes.RemoveAt (index);
				}
				
				PrintList("All without Pedro shuffled", shuffledNonPedroes);
				PriorityQueue targetsQueue = new PriorityQueue (shuffledNonPedroes);		
				allTargets.Add (pedro, ChooseTargets (pedro, targetsQueue));
				foreach (Guy nonPedro in shuffledNonPedroes) {
					allTargets.Add (nonPedro, ChooseTargets (nonPedro, targetsQueue));
				}
				return pedro;
		}
		
		private static void PrintList(string message, List<Guy> guysToPrint) {
				string output = message + ": ";
				foreach (Guy guy in guysToPrint) {								
						output += guy.GetId() + " ";
				}
				Debug.Log (output);
		}

}
