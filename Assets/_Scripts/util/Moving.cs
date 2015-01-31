using UnityEngine;
using System.Collections;

public class Moving : MonoBehaviour {

	public float initVerticalSpeed; // adjust in inspector 
	 
	public float VerticalAccerlation; //adjust in inspector 
	// could be negative 
	public float HorizontalSpeed; // adjust in inspector 
	public float VerticalSpeed; // should be private

	// Update is called once per frame
	void FixedUpdate () {

		transform.position = new Vector2(transform.position.x + HorizontalSpeed * Time.fixedDeltaTime,
		                                 transform.position.y + VerticalSpeed * Time.fixedDeltaTime);

		VerticalSpeed += VerticalAccerlation*Time.fixedDeltaTime;
	}
	


	
	

	

}
