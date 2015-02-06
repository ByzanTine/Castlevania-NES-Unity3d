using UnityEngine;
using System.Collections;

public class OnWhipHitDestroy : OnWhipEvent {

	public GameObject itemPrefab;

	public bool fixedItem = true;

	private bool hitted = false;

	private static GameObject[] randItems;

	public override void onWhipEnter (){
		if(!hitted)
			StartCoroutine (revealItemAndDestroy());
	}


	IEnumerator revealItemAndDestroy()
	{
		hitted = true;
		GameObject deathEffect = Resources.Load ("Prefab/death") as GameObject;
		Instantiate (deathEffect, collider2D.bounds.center, Quaternion.identity);

		randItems = Resources.LoadAll<GameObject>("Prefab/RandItem");
		GameObject hitSE = Resources.Load (Globals.SEdir + "hitSE") as GameObject;
		Instantiate (hitSE, transform.position, Quaternion.identity);

		GameObject pObj = GameObject.FindGameObjectWithTag (Globals.playerTag);
		if(pObj)
		{
			StatusManager smScript = 
				pObj.GetComponent<StatusManager> ();
			if(smScript)
				smScript.score += 100;
		}



		if(!fixedItem)
		{
			int randId = Random.Range(0, randItems.Length);
			itemPrefab = randItems[randId];
			if(itemPrefab.name == "WhipUp")
			{
				WhipAttackManager wamScript = 
					GameObject.FindGameObjectWithTag(Globals.playerTag).GetComponent<WhipAttackManager>();

				if(wamScript.whipLevel == 3)
				{
					itemPrefab = randItems[1];
				}
			}
		}

		Instantiate (itemPrefab, transform.position, Quaternion.identity);
		Destroy (this.gameObject);
		yield return null;
	}
}


