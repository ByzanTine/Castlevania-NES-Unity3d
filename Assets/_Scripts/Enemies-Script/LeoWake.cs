using UnityEngine;
using System.Collections;

public class LeoWake : MonoBehaviour {


	// all public var need to be init in Inspector
	public GameObject  demonPrefab;
	// Use this for initialization
	void Start () {
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			leoWake();
			collider2D.enabled = false;

		}
	}
	
	void leoWake()
	{
		LeoMotion lmScript = GetComponentInParent<LeoMotion>();
		lmScript.wakeUp ();

	}

}
