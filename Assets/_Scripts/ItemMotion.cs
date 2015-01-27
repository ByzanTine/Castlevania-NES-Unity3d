using UnityEngine;
using System.Collections;

public class ItemMotion : MonoBehaviour {

	public float speedY = -0.01f;
	public float perishInSec = 10.0f;

	//must be specified in inspector
	public Globals.ItemName itemName;

	// Use this for initialization
	void Start () {
	
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
			
			CollisionManager cmScript = GetComponent<CollisionManager> ();
			if (cmScript != null) {
				if (cmScript.isWallOn (Globals.Direction.Bottom)) {
					hitGround();
				}
			}
		}
	}

	public void hitGround()
	{
		speedY = 0;
		StartCoroutine (autoDie());
	}

	void OnTriggerEnter2D( Collider2D coll ) {
		GameObject collidedObj = coll.gameObject;
		if (collidedObj.tag == Globals.playerTag) 
		{
			itemPickedUp(collidedObj);		              
		}
	}

	IEnumerator autoDie()
	{
		yield return new WaitForSeconds(perishInSec);
		Destroy (this.gameObject);
	}

	void itemPickedUp(GameObject plObj)
	{
//		StatusManager smScript = plObj.GetComponent<StatusManager> ();
		if (itemName == Globals.ItemName.Money_S) 
		{
			StatusManager.score += 100;
			Debug.Log ("fetched small money");
		} 
		else if (itemName == Globals.ItemName.LargeHeart) 
		{
			StatusManager.heartNum += 5;
			Debug.Log ("fetched heart");
		}

		Debug.Log ("Player picked Up");
		Destroy (this.gameObject);
	}
}
