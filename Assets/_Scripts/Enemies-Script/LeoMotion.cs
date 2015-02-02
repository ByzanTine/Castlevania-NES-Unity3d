using UnityEngine;
using System.Collections;

public class LeoMotion : MonoBehaviour {

	public float initVerticalSpeed; // adjust in inspector 
	public float HorizontalSpeed; // adjust in inspector 
	public float VerticalSpeed; // should be private 
	public float VerticalAccerlation;
	// could be negative 

	private const float perishInSec = 1.0f;
	private bool isAwake;
	private Vector2 speed;

	private bool onGround;
	public bool facingRight = true; // true for right 
	private Animator animator;
	// Use this for initialization
	void Start () {

		isAwake = false;
		VerticalSpeed = 0.0f;
		CollisionManager cmScript = GetComponent<CollisionManager>();
		animator = GetComponent<Animator> ();
		if (!animator) {
			Debug.LogError("animator can't retrieve");
		}
		cmScript.ExitGround += onExitGround;
		cmScript.EnterGround += onEnterGround;

	}
	public void wakeUp()
	{
		isAwake = true;
		animator.SetBool ("Jump", true);
		VerticalSpeed = initVerticalSpeed;

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(isAwake)
			move ();
	}

	void onEnterGround()
	{
		// flip 
		if (isAwake && transform.position.y < 0.1) {
			HorizontalSpeed *= -1;
			VerticalSpeed = 0.0f;
			animator.SetBool ("Jump", false);
			onGround = true;
			Flip ();
		}
	}

	void onExitGround()
	{
		Debug.Log ("exit ground responsed");

		VerticalSpeed = initVerticalSpeed;
		onGround = false;
	}

	void move()
	{
	
		transform.position = new Vector2(transform.position.x + HorizontalSpeed * Time.fixedDeltaTime,
		                                 transform.position.y + VerticalSpeed * Time.fixedDeltaTime);
		if (!onGround)
			VerticalSpeed += VerticalAccerlation*Time.fixedDeltaTime;
	}
	
	
	void OnTriggerEnter2D( Collider2D coll ) {
		GameObject collidedObj = coll.gameObject;
		if (collidedObj.tag == Globals.playerTag) 
		{
			onPlayerEnter(coll.gameObject);		              
		}
//		else if(collidedObj.tag == Globals.basicGroundTag)
//		{
//			Debug.Log("coll baisc ground");
//			speed.x = defaultSpeed.x;
//			speed.y = 0;
//		}

	}
	
	void onPlayerEnter(GameObject gb)
	{
		Debug.Log ("Player hitted");
		PlayerController pcScript = gb.GetComponent<PlayerController> ();
		pcScript.HandleHurt ();
	}

	public void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		
	}
}
