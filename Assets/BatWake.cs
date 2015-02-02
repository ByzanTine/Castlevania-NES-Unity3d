using UnityEngine;
using System.Collections;

public class BatWake : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == Globals.playerTag) 
		{
			batWake();
			collider2D.enabled = false;
		}
	}
	
	void batWake()
	{
		SmallBatMotion sbScript = GetComponentInParent<SmallBatMotion>();
		sbScript.wakeUp ();
	}
}
