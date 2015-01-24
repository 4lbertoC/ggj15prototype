using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Guy : MonoBehaviour {

	private static int guyIdCumulative = 0;

	private int guyId;

	void Awake() {
		guyId = guyIdCumulative++;
	}

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AimAt(List<Guy> guys) {

	}

	public Vector3 GetPosition() {
		return this.transform.position;
	}

	public int GetId() {
		return guyId;
	}
}
