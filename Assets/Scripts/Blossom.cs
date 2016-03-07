using UnityEngine;
using System.Collections;

public class Blossom : MonoBehaviour {

    public int framesToBlossom = 20;
    private int framesSinceStart = 0;

	// Use this for initialization
	void Start () {
        transform.localScale = Vector3.zero;
        framesToBlossom = 60;
        AudioPlayer.GetInstance().PlaySound("Blossom");
    }
	
	// Update is called once per frame
	void Update () {
        if (framesSinceStart++ >= framesToBlossom)
        {
            transform.localScale = Vector3.one;
        }
        else
        {
            transform.localScale = Vector3.one * ((float)framesSinceStart) / framesToBlossom;
        }
	}
}
