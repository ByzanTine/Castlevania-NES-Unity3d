using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CollisionResolve : MonoBehaviour {

	private enum RDirection{Left, Right, Bottom, Top, None};

	public int collIndex;
	//private int playerCollIndex;
	public Hashtable collIdTable = new Hashtable();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D( Collider2D coll ) {
		Debug.Log ("collided");

		GameObject collidedObj = coll.gameObject;


		// get the four corner points of both object
		Vector2 objLL = collidedObj.collider2D.bounds.min;
		Vector2 objUR = collidedObj.collider2D.bounds.max;
		Vector2 myLL = collider2D.bounds.min;
		Vector2 myUR = collider2D.bounds.max;
			

		List<float> collDepth = new List<float> (
			new float[4] {float.MaxValue,float.MaxValue,float.MaxValue,float.MaxValue});

//		if(objUR.x >= myLL.x && objLL.x <= myLL.x)             // Player on left
			collDepth[0] = objUR.x - myLL.x;
//		if(objLL.x <= myUR.x && objUR.x >= myUR.x)             // Player on Right
			collDepth[1] = myUR.x - objLL.x;
//		if(objUR.y>= myLL.y && objLL.y <= myLL.y)             // Player on Bottom
			collDepth[2] = objUR.y- myLL.y;
//		if(objLL.y <= myUR.y && objUR.y>= myUR.y)             // Player on Top
		collDepth[3] = myUR.y - objLL.y - 0.02f;


		
		// return the closest intersection
		collIndex = collDepth.IndexOf(Mathf.Min(collDepth.ToArray()));

		CollisionManager cmScript = collidedObj.GetComponent<CollisionManager>();
		if (cmScript != null) {

			if (collidedObj.tag == Globals.playerTag) {
				StairManager smScript = collidedObj.GetComponent<StairManager>();
				if(smScript && smScript.isOnStair())
				{
					collIndex = 3;
				}
			}
			cmScript.playerCollisionEnter (collIndex, this.gameObject.collider2D.bounds.max.y);
			collIdTable.Add(collidedObj.GetInstanceID(), collIndex);
		}
	}
	
	void OnTriggerExit2D( Collider2D coll ) {
		GameObject collidedObj = coll.gameObject; 
		releaseItem (collidedObj);

	}

	void OnDestroy()
	{
		GameObject playerObj = GameObject.FindGameObjectWithTag(Globals.playerTag);
		if(playerObj && collIdTable.Contains(playerObj.GetInstanceID()))
		{
			releaseItem (GameObject.FindGameObjectWithTag(Globals.playerTag));
		}
	}

	void releaseItem(GameObject collidedObj)
	{	
		CollisionManager cmScript = collidedObj.GetComponent<CollisionManager>();
		if (cmScript != null) {
			cmScript.playerCollisionExit ((int)collIdTable[collidedObj.GetInstanceID()]);
			collIdTable.Remove(collidedObj.GetInstanceID());
		}
	}
}
