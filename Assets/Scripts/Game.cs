﻿using UnityEngine;
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
						Guy targetGuy = otherGuys.GetAndTouchHead () as Guy;
						targets.Add (targetGuy);
						shootingGuy.AimAt (targetGuy);
						--targetsCount;
				}
				return targets;
		}

		public static Guy FindPedro (List<Guy> guys)
		{
				Dictionary<Guy, List<Guy>> allTargets = new Dictionary<Guy, List<Guy>> ();
				List<Guy> guysClone = new List<Guy> (guys);

				int freeGuyIndex = (int)Mathf.Floor (Random.value * guysClone.Count);
				Guy freeGuy = guysClone [freeGuyIndex];
				Debug.Log (freeGuy.GetId () + " is free!");

				guysClone.Remove (freeGuy);
				PriorityQueue sortedGuys = new PriorityQueue (guysClone);

				allTargets.Add (freeGuy, ChooseTargets (freeGuy, sortedGuys));

				foreach (Guy g in guysClone) {
						allTargets.Add (g, ChooseTargets (g, sortedGuys));
				}

				return freeGuy;
		}
}
