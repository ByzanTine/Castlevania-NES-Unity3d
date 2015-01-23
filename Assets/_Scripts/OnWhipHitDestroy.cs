using UnityEngine;
using System.Collections;

public class OnWhipHitDestroy : OnWhipEvent {

	// Use this for initialization
	void Start () {
	
	}

	public override void onWhipEnter (){
		StartCoroutine (revealItemAndDestroy());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator revealItemAndDestroy()
	{
		yield return new WaitForSeconds(0.1f);
		Destroy (this.gameObject);
	}
}


