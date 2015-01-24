using UnityEngine;
using System.Collections;

public class Arm : MonoBehaviour
{

		public Transform target = null;
		public float speed = 0.1f; // in radians/sec

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (target != null) {
						Vector3 targetDir = target.position - transform.position;
						targetDir.z = 0;
						float step = speed * Time.deltaTime;
						Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
						// Debug.DrawRay(transform.position, newDir, Color.red);
						transform.rotation = Quaternion.LookRotation (newDir);
				}
		}
	
		public void Hide ()
		{
				gameObject.SetActive (false);
		}
	
		public void Show ()
		{
				gameObject.SetActive (true);
		}
}
