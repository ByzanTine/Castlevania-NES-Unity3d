using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour {
	private SpriteRenderer spri;
	// Use this for initialization
	void Start () {
		spri = GetComponent<SpriteRenderer> ();
	}
	
	// legacy 
//	void OnTriggerEnter2D(Collider2D other) {
//		Debug.Log ("Something enter me");
//		StartCoroutine (changeColor ());
//	}

	public IEnumerator changeColor() {
		spri.color = Color.red;
		yield return new WaitForSeconds(0.5f);
		spri.color = Color.white;

	}
}
