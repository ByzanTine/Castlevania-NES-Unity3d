using UnityEngine;
using System.Collections;

public class ShootFireBall : MonoBehaviour {
	public GameObject fireball;

	private Animator animator;

	private float curAngle = 10.0f;
	private float angleIncrement = 10.0f;
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();

	}

	public void wakeUp (float startTime, bool sprial, float Rate, float Angle) {
		angleIncrement = Angle;
		if (!sprial) {
			InvokeRepeating ("Shoot",startTime, Rate);
		}
		else {
			InvokeRepeating ("ShootSprial",startTime, Rate);
		}
		animator.SetBool ("Shoot", true);
		curAngle = Random.Range (0, 360.0f);
	}



	void Shoot() {
		GameObject gb = Instantiate (fireball, transform.position, Quaternion.identity) as GameObject;
		// follow transform
		Vector3 rotate = new Vector3 (-1.0f, 0.0f, 0.0f).normalized;
		Vector3 rotation = transform.rotation * rotate;
		gb.rigidbody2D.velocity = rotation;


	}



	void ShootSprial () {
		GameObject gb = Instantiate (fireball, transform.position, Quaternion.identity) as GameObject;


	
		Vector3 rotate = new Vector3 (-1.0f, 0.0f, 0.0f).normalized;
		Vector3 rotation = Quaternion.Euler(0, 0, curAngle) * rotate;
		transform.rotation = Quaternion.Euler (0, 0, curAngle);
		gb.rigidbody2D.velocity = rotation;
		curAngle = (curAngle + angleIncrement) % 360.0f;

	}
}
