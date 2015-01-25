using UnityEngine;
using System.Collections;

public class Body2 : MonoBehaviour
{

		public void Start ()
		{
				gameObject.GetComponentInChildren<SpriteRenderer> ().enabled = false;
		}

		public void Hide ()
		{
				gameObject.GetComponentInChildren<SpriteRenderer> ().enabled = false;
		}
	
		public void Show ()
		{
				gameObject.GetComponentInChildren<SpriteRenderer> ().enabled = true;
		}
}
