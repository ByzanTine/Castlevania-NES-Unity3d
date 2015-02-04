using UnityEngine;
using System.Collections;

public class GoingToHell : MonoBehaviour {

	// Use this for initialization
	void OnTriggerEnter2D( Collider2D coll ) {

		GameObject collidedObj = coll.gameObject;
		if(collidedObj.tag == "Player")
		{
			StatusManager stScript = collidedObj.GetComponent<StatusManager>();
			stScript.playerDie();
		}
		else
		{
			Debug.Log("item destroyed:" + collidedObj.name);
			Destroy(collidedObj);
		}

	}
}
