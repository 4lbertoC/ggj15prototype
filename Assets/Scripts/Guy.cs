﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Guy : MonoBehaviour
{

		public GameObject armPrefab;
		private static int guyIdCumulative = 0;
		private int guyId;

		private GameState gameState = GameState.GetInstance();

		void Awake ()
		{
				guyId = guyIdCumulative++;
		}

		// Use this for initialization
		void Start ()
		{
		}
	
		// Update is called once per frame
		void Update ()
		{
			
		}

		void OnMouseDown ()
		{
				bool isPedro = gameState.SelectGuy (this);
				Debug.Log ("Clicked " + guyId + ". " + (isPedro ? "Is Pedro!" : ""));
		}

		public void AimAt (Guy targetGuy)
		{
				GameObject arm = Instantiate (armPrefab, this.GetPosition (), Quaternion.identity) as GameObject;
				arm.transform.parent = this.transform;

				arm.transform.rotation = Quaternion.LookRotation (targetGuy.GetPosition () - this.GetPosition ());
		}

		public Vector3 GetPosition ()
		{
				return this.transform.position;
		}

		public int GetId ()
		{
				return guyId;
		}

}
