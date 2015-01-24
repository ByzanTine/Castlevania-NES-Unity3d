﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CollisionResolve : MonoBehaviour {

//	objUR.xotected GameObject collidedObj;
	enum Direction{Left, Right, Bottom, Top, None};

	private int collIndex;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D( Collider2D coll ) {
		Debug.Log ("collided");

		GameObject collidedObj = coll.gameObject;

		Vector2 objLL = collidedObj.collider2D.bounds.min;
		Vector2 objUR = collidedObj.collider2D.bounds.max;

		Vector2 myLL = collider2D.bounds.min;
		Vector2 myUR = collider2D.bounds.max;

//		Debug.Log ("objxy:" + objLL.x + objLL.y + "myxy:" + myLL.x + myLL.y);
	

		List<float> collDepth = new List<float> (
			new float[4] {float.MaxValue,float.MaxValue,float.MaxValue,float.MaxValue});

//		if(objUR.x >= myLL.x && objLL.x <= myLL.x)             // Player on left
			collDepth[0] = objUR.x - myLL.x;
//		if(objLL.x <= myUR.x && objUR.x >= myUR.x)             // Player on Right
			collDepth[1] = myUR.x - objLL.x;
//		if(objUR.y>= myLL.y && objLL.y <= myLL.y)             // Player on Bottom
			collDepth[2] = objUR.y- myLL.y;
//		if(objLL.y <= myUR.y && objUR.y>= myUR.y)             // Player on Top
			collDepth[3] = myUR.y - objLL.y;
		
		// return the closest intersection
		collIndex = collDepth.IndexOf(Mathf.Min(collDepth.ToArray()));
//		for (int i=0; i<4; ++i)
//			Debug.Log (i + "=" + collDepth[i]);

//		Debug.Log ("" + " c@ " + ((Direction)collIndex).ToString() );

		if (collidedObj.tag == "Player") 
		{
			PlayerCollisionManager plScript = collidedObj.GetComponent<PlayerCollisionManager>();
			plScript.playerCollisionEnter(collIndex);
		}
		else if(collidedObj.tag == "Item")
		{
			ItemMotion itScript = collidedObj.GetComponent<ItemMotion>();
			itScript.hitGround();
		}
		// will be depercated
		collWithPlayer (collidedObj, (Direction)collIndex);
	}

	void collWithPlayer(GameObject playerObj, Direction dir)
	{
		PlayerController plScript = playerObj.GetComponent<PlayerController>();
		switch (dir) 
		{
		case Direction.Bottom:
			break;
		case Direction.Left:
			break;

		case Direction.Right:
//			if(plScript.CurHorizontalVelocity < 0)
//			{
//				//				print ("bool" + plScript.facingRight);
//				plScript.CurHorizontalVelocity = 1;
//			}
			break;

		case Direction.Top:
			if(plScript.VerticalSpeed < 0)
			{
				plScript.VerticalSpeed = 0;
				plScript.grounded = true;
			}
			break;
		default:
			break;
		}


	}

	void OnTriggerExit2D( Collider2D coll ) {
		GameObject collidedObj = coll.gameObject;  
		//collidedObj.GetInstanceID
		if (collidedObj.tag == "Player") 
		{
			PlayerCollisionManager plScript = collidedObj.GetComponent<PlayerCollisionManager>();
			plScript.playerCollisionExit(collIndex);

			// will be deprecated
			if(!plScript.isWallOnBottom())
			{			
				PlayerController plScript2 = collidedObj.GetComponent<PlayerController>();
				plScript2.grounded = false;
			}
		}
	}
}