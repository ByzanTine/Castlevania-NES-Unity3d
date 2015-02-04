using UnityEngine;
using System.Collections;

public class OnWhipHitDestroy : OnWhipEvent {

	public GameObject itemPrefab;

	public bool fixedItem = true;

	private bool hitted = false;

	private static GameObject[] randItems;

	void Start()
	{

	}


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

		randItems = Resources.LoadAll<GameObject>("Prefab/RandItem");
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


