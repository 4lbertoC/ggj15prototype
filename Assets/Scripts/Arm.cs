using UnityEngine;
using System.Collections;

public class Arm : MonoBehaviour
{

		public Guy target = null;
		public float speed = 1.0f; // in radians/sec
		public GameObject bulletPrefab;
		private bool canShoot = true;

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
			Vector3 targetDir;
			float step = speed * Time.deltaTime;
			if (target != null) {
				targetDir = target.transform.position - transform.position;
				targetDir.z = 0;					
			} else {
				targetDir = new Vector3(0f, 0f, 100f);					
			}
			Vector3 newDir = Vector3.RotateTowards (transform.forward, targetDir, step, 0.0F);
			// Debug.DrawRay(transform.position, newDir, (target == null ? Color.red : Color.yellow);
			transform.rotation = Quaternion.LookRotation (newDir);
		}
	
		public void Hide ()
		{
				gameObject.SetActive (false);
		}
	
		public void Show ()
		{
				gameObject.SetActive (true);
		}

		public void Shoot (float bulletSpeed, Guy specialGuy)
		{
				if (target == null) {
						return;
				}
				if (canShoot) {
					canShoot = false;
				} else {
					return;
				}
				Bullet bullet = (Instantiate (bulletPrefab, 
	             		transform.position,
                  		new Quaternion(0f, 0f, 0f, 0f)) as GameObject)
                  		.GetComponent<Bullet>();
		        bullet.target = this.target;
				bullet.speed = bulletSpeed;
				bullet.specialGuy = specialGuy;
	}

	public void ShootWithDelay (float s, Guy specialGuy)
	{
		StartCoroutine(DelayBeforeShootCoroutine(s, specialGuy));
	}
	
	IEnumerator DelayBeforeShootCoroutine (float bulletSpeed, Guy specialGuy)
	{
			yield return new WaitForSeconds(0.2f);			
			Shoot (bulletSpeed, specialGuy);
	}
}
