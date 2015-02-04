using UnityEngine;
using System.Collections;

public class ThrowBatControl : MonoBehaviour {
	private Animator anim;
	// Use this for initialization
	void Start () {
		GetComponent<Animator> ().SetBool ("Fly", true);
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
