using UnityEngine;
using System.Collections;

public class OnWhipHitDestroy : OnWhipEvent {

	public GameObject itemPrefab;
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
		GameObject itemObj = Instantiate (itemPrefab) as GameObject;
		itemObj.transform.position = this.transform.position;
		Destroy (this.gameObject);

	}
}


