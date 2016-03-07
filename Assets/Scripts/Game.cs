using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Game
{

		public const int ARMS_COUNT = 2;
		private const float LIKELIHOOD_OF_MORE_THAN_ONE_ARM = 0.2f;

    private static bool ChooseTargets(Guy shootingGuy, IList<Guy> untargetedGuys, IList<Guy> otherGuys, Dictionary<Guy, List<Guy>> allTargets)
    {
        List<Guy> targets = new List<Guy>(ARMS_COUNT);
        for (int armIndex = 0; armIndex < ARMS_COUNT; armIndex++)
        {
            if (armIndex == 0 || Random.value < LIKELIHOOD_OF_MORE_THAN_ONE_ARM)
            {
                bool targetFound = false;
                foreach (Guy targetGuy in untargetedGuys)
                {
                    if (shootingGuy != targetGuy && shootingGuy.CanSee(targetGuy))
                    {
                        targetFound = true;
                        targets.Add(targetGuy);
                        untargetedGuys.Remove(targetGuy);
                        bool ok = ChooseTargets(targetGuy, untargetedGuys, otherGuys, allTargets);
                        if (!ok)
                        {
                            return false;
                        }
                        break;
                    }
                }
                if (!targetFound)
                {
                    for (int i = 0; i < otherGuys.Count; i++)
                    {
                        Guy targetGuy = otherGuys[i];
                        if (shootingGuy != targetGuy && shootingGuy.CanSee(targetGuy))
                        {
                            targetFound = true;
                            targets.Add(targetGuy);
                            // ChooseTargets(targetGuy, untargetedGuys, otherGuys, allTargets);
                            break;
                        }
                    }
                }
                if (!targetFound)
                {
                    Debug.LogError(shootingGuy.name + " couldn't find anybody to shoot at");
                    return false;
                }
            }
        }
        allTargets.Add(shootingGuy, targets);
        return true;
    }    

		public static Guy FindPedro (List<Guy> guys)
		{
                Dictionary<Guy, List<Guy>> allTargets;
                Guy pedro;
                bool goodConfigurationFound = false;
                do
                {
                    allTargets = new Dictionary<Guy, List<Guy>>();
                    List<Guy> nonPedroes = new List<Guy>(guys);
                    PrintList("All", nonPedroes);

                    int pedroIndex = (int)Mathf.Floor(Random.value * guys.Count);
                    pedro = nonPedroes[pedroIndex];
                    // pedro.RandomSpeak ();
                    Debug.Log(pedro.GetId() + " is Pedro!");
                    nonPedroes.Remove(pedro);
                    PrintList("All without Pedro", nonPedroes);

                    List<Guy> shuffledNonPedroes = new List<Guy>();
                    while (nonPedroes.Count > 0)
                    {
                        int index = (int)Mathf.Floor(Random.value * nonPedroes.Count);
                        shuffledNonPedroes.Add(nonPedroes[index]);
                        nonPedroes.RemoveAt(index);
                    }

                    PrintList("All without Pedro shuffled", shuffledNonPedroes);                    
                    nonPedroes = new List<Guy>(shuffledNonPedroes);
                    if (ChooseTargets(pedro, shuffledNonPedroes, nonPedroes, allTargets))
                    {
                        if (allTargets.Count == guys.Count)
                        {
                            Debug.Log("Good standoff configuration found.");
                            goodConfigurationFound = true;
                        }
                        else
                        {
                            Debug.LogWarning("Bad standoff configuration found. Recomputing");
                        }
                        
                    }
                    else
                    {
                        Debug.LogWarning("Very bad standoff configuration found. Recomputing");
                    }
                } while (!goodConfigurationFound);
                foreach (Guy shootingGuy in allTargets.Keys)
                {
                    List<Guy> targetsForThisGuy = allTargets[shootingGuy];
                    for (int armIndex = 0; armIndex < ARMS_COUNT; armIndex++)
                    {
                        if (armIndex < targetsForThisGuy.Count)
                        {
                            Guy targetGuy = targetsForThisGuy[armIndex];
                            if (GameState.GetInstance().IsIntro())
                            {
                                shootingGuy.WaitAndSuddenlyAimAt(armIndex, targetGuy);
                            }
                            else {
                                shootingGuy.AimAt(armIndex, targetGuy);
                            }
                        }
                        else {
                            shootingGuy.AimAtNobody(armIndex);
                        }
                    }
                }
				return pedro;
		}
		
		public static void SalvationFor(Guy runner) {
				runner.ShutUp ();
				runner.RemoveFromScene ();
		}
		
		public static void ShootingSpree(Guy protagonist, Guy shooter) {
				if (shooter != protagonist) {
						shooter.Chase(protagonist);
				}
				shooter.ShootButRememberThatGuyIsSpecial (protagonist);
				protagonist.ShowPlayButtonAfterDeath ();
		}
		
		public static IEnumerator LossSequenceCoupDeGrace ()
		{
				GameState gameState = GameState.GetInstance ();
				if (gameState.IsOutro()) {						
                        yield return new WaitWhile(gameState.AreBulletsFlying);
                        List<Guy> survivors = gameState.GetSurvivorsWithGuns();
                        if (survivors.Count > 1) {
								Debug.Log ("Coup de grace needed, " + survivors.Count + " survivors.");
								foreach (Guy guy in survivors) {
										guy.ShootButRememberThatGuyIsSpecial(null);
								}
						}  else {
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

		public static void VictorySequence (List<Guy> nonPedroes, List<Guy> savedGuys)
		{
				Debug.Log ("victory!!!");
				Guy juan = nonPedroes[0];
				Guy gonzalo = nonPedroes[1];
				// Question balloon should be left of the answer balloon:
				if (juan.transform.position.x < gonzalo.transform.position.x) {
						juan.ShowAndNowMessage (gonzalo, savedGuys);
				} else {
						gonzalo.ShowAndNowMessage (juan, savedGuys);
				}
				
		}
}
