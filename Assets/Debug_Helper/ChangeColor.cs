using UnityEngine;
using System.Collections;

public class ChangeColor : MonoBehaviour {
	private SpriteRenderer spri;
	// Use this for initialization
	void Start () {
		spri = GetComponent<SpriteRenderer> ();
	}
	

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log ("PLayer enter me");
		StartCoroutine (changeColor ());
	}

	IEnumerator changeColor() {
		spri.color = Color.red;
		yield return new WaitForSeconds(0.5f);
		spri.color = Color.white;

	}
}
