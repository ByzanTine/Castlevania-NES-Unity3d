using UnityEngine;
using System.Collections;

public class HiddenTreasure : OnWhipEvent {

	public GameObject itemPrefab;
	public float horShift = 0;
	public float horShrink = 0;
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

		GameObject hitSE = Resources.Load (Globals.SEdir + "hitSE") as GameObject;
		Instantiate (hitSE, transform.position, Quaternion.identity);

		
		Instantiate (itemPrefab, transform.position, Quaternion.identity);
		
		shrinkCollider();
		yield return null;
	}

	void shrinkCollider()
	{
		transform.position += new Vector3 (horShift, 0, 0);
		Vector3 temp = collider2D.bounds.size - new Vector3 (horShrink, 0, 0);
		//Bounds tempBounds = collider2D.bounds;
	}
}
