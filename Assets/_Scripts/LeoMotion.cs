using UnityEngine;
using System.Collections;

public class LeoMotion : MonoBehaviour {
	
	private static Vector2 defaultSpeed = new Vector2 (-0.02f, -0.02f);
	private const float perishInSec = 1.0f;
	private bool isAwake = false;
	private Vector2 speed = new Vector2(0.0f, defaultSpeed.y);

//	private bool onGround = false;
	public bool isMoveLeft = true;

	// Use this for initialization
	void Start () {
		if (!isMoveLeft)
			defaultSpeed.x *= -1;
		CollisionManager cmScript = GetComponent<CollisionManager>();
		cmScript.ExitGround += onExitGround;
		cmScript.EnterGround += onEnterGround;

	}
	public void wakeUp()
	{
		isAwake = true;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(isAwake)
			move ();
	}

	void onEnterGround()
	{
		Debug.Log ("enter ground responsed");
		if(transform.position.y < 0.5f)
			defaultSpeed.x *= -1.0f;
		speed.x = defaultSpeed.x;
		speed.y = 0;
	}

	void onExitGround()
	{
		Debug.Log ("exit ground responsed");
		speed.x = defaultSpeed.x/2;
		speed.y = defaultSpeed.y;
	}

	void move()
	{
		Vector3 pos = transform.position;
		pos.x += speed.x;
		pos.y += speed.y;
		transform.position = pos;
	}
	
	
	void OnTriggerEnter2D( Collider2D coll ) {
		GameObject collidedObj = coll.gameObject;
		if (collidedObj.tag == Globals.playerTag) 
		{
			onPlayerEnter(coll.gameObject);		              
		}
	}
	
	void onPlayerEnter(GameObject gb)
	{
		Debug.Log ("Player hitted");
		gb.GetComponent<StatusManager>().playerHealth -= 2;
	}
}
