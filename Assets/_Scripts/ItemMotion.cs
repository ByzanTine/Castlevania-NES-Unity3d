using UnityEngine;
using System.Collections;

public class ItemMotion : MonoBehaviour {

	public float speedY = -0.01f;
	public float perishInSec = 10.0f;

	//must be specified in inspector
	public Globals.ItemName itemName;

	private StatusManager status;
	private CollisionManager cmScript;
	// Use this for initialization
	void Start () {
		cmScript = GetComponent<CollisionManager> ();
		if (!cmScript)
			Debug.LogError("CollisionManager can't retrieve, check your prefab if it's linked");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		move ();
	}

	void move()
	{
		if (speedY < 0) {
			Vector3 pos = this.transform.position;
			pos.y += speedY;
			this.transform.position = pos;

			if (cmScript.isWallOn (Globals.Direction.Bottom)) {
				hitGround();
			}

		}
	}

	public void hitGround()
	{
		speedY = 0;
		Destroy (this.gameObject, perishInSec);
	}

	void OnTriggerEnter2D( Collider2D coll ) {
		GameObject collidedObj = coll.gameObject;
		if (collidedObj.tag == Globals.playerTag) 
		{
			itemPickedUp(collidedObj);		              
		}
	}



	void itemPickedUp(GameObject plObj)
	{
		StatusManager smScript = plObj.GetComponent<StatusManager> ();
		if (itemName == Globals.ItemName.Money_S) 
		{
			smScript.score += 100;
			Debug.Log ("fetched small money");
		} 
		else if (itemName == Globals.ItemName.LargeHeart) 
		{
			smScript.heartNum += 5;
			Debug.Log ("fetched heart");
		}

		Debug.Log ("Player picked Up");
		Destroy (this.gameObject);
	}
}
