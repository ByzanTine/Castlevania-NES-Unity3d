using UnityEngine;
using System.Collections;

public class ZombieMotion : MonoBehaviour {

	private static Vector2 defaultSpeed = new Vector2 (-0.01f, -0.015f);
	private const float perishInSec = 1.0f;

	private Vector2 speed = new Vector2(0.0f, defaultSpeed.y);
	public bool isMoveLeft = true;

	// Use this for initialization
	void Start () {
		if (!isMoveLeft)
			defaultSpeed.x *= -1;
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
			onPlayerEnter(coll.gameObject);		              
		}
	}
	
	
	IEnumerator autoDie()
	{
		yield return new WaitForSeconds(perishInSec);
		Destroy (this.gameObject);
	}
	
	void onPlayerEnter(GameObject gb)
	{
		Debug.Log ("Player hitted");
		PlayerController pcScript = gb.GetComponent<PlayerController> ();
		pcScript.HandleHurt ();
	}
}
