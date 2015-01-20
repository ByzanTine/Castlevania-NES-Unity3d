using UnityEngine;
using System.Collections;

public class ChangeColor : OnWhipEvent {
	private SpriteRenderer spri;
	// Use this for initialization
	void Start () {
		spri = GetComponent<SpriteRenderer> ();
	}

	public override void onWhipEnter (){
		StartCoroutine (changeColor());
	}

	public IEnumerator changeColor() {
		spri.color = Color.red;
		yield return new WaitForSeconds(0.5f);
		spri.color = Color.white;

	}
}
