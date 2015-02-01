using UnityEngine;
using System.Collections;

public class OnWhipHitDestroy : OnWhipEvent {

	public GameObject itemPrefab;

	private bool hitted = false;


	public override void onWhipEnter (){
		if(!hitted)
			StartCoroutine (revealItemAndDestroy());
	}


	IEnumerator revealItemAndDestroy()
	{
		hitted = true;
		GameObject deathEffect = Resources.Load ("Prefab/death") as GameObject;
		Instantiate (deathEffect, collider2D.bounds.center, Quaternion.identity);

		yield return new WaitForSeconds (0.1f);
		Instantiate (itemPrefab, transform.position, Quaternion.identity);

		Destroy (this.gameObject);
		yield return null;

	}
}


