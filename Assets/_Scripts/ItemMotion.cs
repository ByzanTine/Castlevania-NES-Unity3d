using UnityEngine;
using System.Collections;

public class ItemMotion : MonoBehaviour {

	public float speedY = -0.01f;
	public float perishInSec = 10.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 pos = this.transform.position;
		pos.y += speedY;
		this.transform.position = pos;
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
			itemPickedUp();		              
		}
	}


	IEnumerator autoDie()
	{
		yield return new WaitForSeconds(perishInSec);
		Destroy (this.gameObject);
	}

	void itemPickedUp()
	{
		Debug.Log ("Player picked Up");
		Destroy (this.gameObject);
	}
}
