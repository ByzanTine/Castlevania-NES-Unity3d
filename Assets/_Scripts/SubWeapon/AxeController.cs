using UnityEngine;
using System.Collections;

public class AxeController : MonoBehaviour {
	private Vector3 speed = new Vector3(1.6f, 3.6f, 0.0f);
	private float gravity = 10f;
	// Use this for initialization
	void Start () {
		speed.x *= transform.localScale.x;
	}
	
	void FixedUpdate () {
		transform.position 
			= transform.position + speed * Time.fixedDeltaTime;
		speed.y -= gravity * Time.fixedDeltaTime;
	}
	
	void OnTriggerEnter2D( Collider2D coll ) {
		GameObject collidedObj = coll.gameObject;
		OnWhipEvent whipScript = collidedObj.GetComponent<OnWhipEvent>();

		if(whipScript != null)
		{
			whipScript.onWhipEnter();
			GameObject deathEffect = Resources.Load ("Prefab/death") as GameObject;
			Instantiate (deathEffect, transform.position, Quaternion.identity);
		}
	}
}
