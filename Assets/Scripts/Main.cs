using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Main : MonoBehaviour
{

		public GameObject guyPrefab;
		public GameObject housePrefab;
		public Restarter playButton;
		private List<Vector3> positions = new List<Vector3> ();
		private Quaternion defaultRotation = Quaternion.Euler (270, 0, 0);
		private GameState gameState = GameState.GetInstance ();

        private List<Guy> guysToInit = new List<Guy>();

        void Awake ()
		{
        /*
				positions.Add (new Vector3 (-3.36f, 3.98f, 0.68f));
				positions.Add (new Vector3 (2.65f, 3.78f, 2.62f));
				positions.Add (new Vector3 (-6.26f, 0.5f, -0.23f));
				positions.Add (new Vector3 (-0.61f, 0.53f, 0.98f));
				positions.Add (new Vector3 (4.62f, 2.14f, 0f));
				positions.Add (new Vector3 (-2.52f, -3.06f, -1.785f));
				positions.Add (new Vector3 (1.25f, -3.2f, -4.3f));
				positions.Add (new Vector3 (5.26f, -1.69f, -1.06f));
          */

                positions.Add(new Vector3(-3.36f, 3.98f, 0));
                positions.Add(new Vector3(2.65f, 3.78f, 0));
                positions.Add(new Vector3(-6.26f, 0.5f, 0));
                positions.Add(new Vector3(-0.61f, 0.53f, 0));
                positions.Add(new Vector3(4.62f, 2.14f, 0));
                positions.Add(new Vector3(-2.52f, -3.06f, 0));
                positions.Add(new Vector3(1.25f, -3.2f, 0));
                positions.Add(new Vector3(5.26f, -1.69f, 0));
                positions.Add(new Vector3(-7.49f, 3.4f, 0));
                positions.Add(new Vector3(7.57f, 4.05f, 0));
                positions.Add(new Vector3(-0.92f, 3.14f, 0));
                positions.Add(new Vector3(-2.82f, -0.62f, 0));
                positions.Add(new Vector3(-6.75f, -3.71f, 0));

        AudioPlayer audioPlayer = GameObject.FindGameObjectWithTag ("AudioController").GetComponent<AudioPlayer> ();
				gameState.SetAudioPlayer (audioPlayer);
		}

		private void PrintCombinations (Dictionary<Guy, List<Guy>> allTargets)
		{
				foreach (KeyValuePair<Guy, List<Guy>> p in allTargets) {
						int guyId = ((Guy)p.Key).GetId ();
						string others = "";
						foreach (Guy g in p.Value) {
								others += g.GetId () + ", ";
						}
	
						Debug.Log (guyId + " aims at " + others);
				}
		}

		public void Restart ()
		{
				playButton.Hide ();
				gameState.Clear ();

                for (int i = 0; i < positions.Count; i++)
                {
                    GameObject guyGO = (GameObject)Instantiate(guyPrefab, positions[i], defaultRotation);
                    Instantiate(housePrefab, positions[i], Quaternion.identity);
                    guyGO.transform.parent = this.transform;
                    Guy guy = guyGO.GetComponent<Guy>();
                    guysToInit.Add(guy);
                    guyGO.name = "Guy #" + guy.GetId();
                }
        }
	
		// Use this for initialization
		void Start ()
		{
				Restart ();
		}

		// Update is called once per frame
		void Update ()
		{
            if (guysToInit.Count > 0)
            {
                List<Guy> guys = new List<Guy>();             
                foreach (Guy guy in guysToInit)
                {
                    bool seesSomebody = false;
                    foreach (Guy otherGuy in guysToInit)
                    {
                        if (guy.CanSee(otherGuy)) {
                            seesSomebody = true;
                        }    
                    }
                    if (!seesSomebody)
                    {
                        Debug.LogError(guy.name + " cannot see anybody");
                        return;
                    }
                    guys.Add(guy);
                }
                gameState.Init(guys);
                SendMessage("OnGuysUpdate");
                guysToInit.Clear();
            }
	
		}

		void OnTimerEnded ()
		{
				Debug.Log ("Timer Ended!!!");
		}
}
