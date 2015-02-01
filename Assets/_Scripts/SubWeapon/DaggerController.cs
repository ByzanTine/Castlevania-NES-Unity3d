using UnityEngine;
using System.Collections;

public class DaggerController : MonoBehaviour {
	public float Speed;
	// Use this for initialization
	void Start () {
		// flip first 
		transform.localScale = new Vector3 (-1 * transform.localScale.x, 
		                                    transform.localScale.y,
		                                    transform.localScale.z);
	}

	void FixedUpdate () {
		transform.position = new Vector2 (transform.position.x + Speed * Time.fixedDeltaTime * transform.localScale.x,
		                                  transform.position.y);
	}



}
