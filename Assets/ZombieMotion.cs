using UnityEngine;
using System.Collections;

public class ZombieMotion : MonoBehaviour {

	private static Vector2 defaultSpeed = new Vector2 (-0.01f, -0.015f);
	private const float perishInSec = 1.0f;

	public Vector2 speed = new Vector2(0.0f, defaultSpeed.y);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		move ();
	}

	void move()
	{
		Vector3 pos = this.transform.position;
		pos.x += speed.x;
		pos.y += speed.y;
		this.transform.position = pos;
		
		CollisionManager cmScript = GetComponent<CollisionManager>();
		if (cmScript != null) 
		{
			if(cmScript.isWallOn(Globals.Direction.Bottom))
			{
				speed.y = 0;
				speed.x = defaultSpeed.x;
				if(cmScript.isWallOn(Globals.Direction.Left))
				{
					speed.x = 0;
					StartCoroutine (autoDie());
				}
			}
			else
			{
				speed.x = 0;
				speed.y = defaultSpeed.y;
			}
		}
	}

	
	void OnTriggerEnter2D( Collider2D coll ) {
		GameObject collidedObj = coll.gameObject;
		if (collidedObj.tag == Globals.playerTag) 
		{
			onPlayerEnter();		              
		}
	}
	
	
	IEnumerator autoDie()
	{
		yield return new WaitForSeconds(perishInSec);
		Destroy (this.gameObject);
	}
	
	void onPlayerEnter()
	{
		Debug.Log ("Player hitted");
		StatusManager.playerHealth -= 2;
		OnWhipHitDestroy owhScript = GetComponent<OnWhipHitDestroy>();
		owhScript.onWhipEnter();
	}
}
