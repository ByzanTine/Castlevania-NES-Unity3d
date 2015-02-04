using UnityEngine;
using System.Collections;

public class DaggerController : MonoBehaviour {
	public float Speed;
	// Use this for initialization
	void Start () {
	}

	void FixedUpdate () {
		transform.position = new Vector2 (transform.position.x + Speed * Time.fixedDeltaTime * transform.localScale.x,
		                                  transform.position.y);
	}

	void OnTriggerEnter2D( Collider2D coll ) {
		GameObject collidedObj = coll.gameObject;
		OnWhipEvent whipScript = collidedObj.GetComponent<OnWhipEvent>();

		if(whipScript != null)
		{
			whipScript.onWhipEnter();

			Destroy(this.gameObject);
		}
	}	
}
