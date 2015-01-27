using UnityEngine;
using System.Collections;

public class OnWhipHitDestroy : OnWhipEvent {

	public GameObject itemPrefab;
	private bool hitted = false;
	// Use this for initialization
	void Start () {
	
	}

	public override void onWhipEnter (){
		if(!hitted)
			StartCoroutine (revealItemAndDestroy());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator revealItemAndDestroy()
	{
		hitted = true;
		GameObject itemObj = Instantiate (itemPrefab) as GameObject;
		itemObj.transform.position = this.transform.position;
		yield return new WaitForSeconds(0.1f);
		Destroy (this.gameObject);
	}
}


