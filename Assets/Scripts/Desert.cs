using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Desert : MonoBehaviour {

    public string[] backgrounds;

	// Use this for initialization
	void Start () {
        transform.Find(backgrounds[Main.gameLevel]).gameObject.SetActive(true);
        GameState.KillInstance();
        // AudioPlayer.GetInstance().PlayMusic(0);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnMouseDown()
    {
        SceneManager.LoadScene("DemoScene");
    }
}
