using UnityEngine;
using System.Collections;

public class HolyWaterController : MonoBehaviour {

	private Vector3 speed = new Vector3(0.8f, 1.0f, 0.0f);
	private float gravity = 5f;
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

			GameObject deathEffect = Resources.Load ("Prefab/holyFire") as GameObject;
			Instantiate (deathEffect, collider2D.bounds.center, Quaternion.identity);
			Destroy(this.gameObject);
		}
		else if(collidedObj.tag == Globals.groundTag)
		{
			GameObject deathEffect = Resources.Load ("Prefab/holyFire") as GameObject;
			Instantiate (deathEffect, collider2D.bounds.center, Quaternion.identity);
			Destroy(this.gameObject);
		}
	}
}
