using UnityEngine;
using System.Collections;

public class SmallBatMotion : MonoBehaviour {
	
	public float HorizontalSpeed; // adjust in inspector 
	public float VerticalSpeed; // should be private 

	// could be negative 
	
	private const float perishInSec = 1.0f;
	private bool isAwake;
	private Vector2 speed;
	
	private bool onGround;
	public bool facingRight; // true for right 
	private Animator animator;
	// Use this for initialization
	void Start () {
		
		isAwake = false;
		VerticalSpeed = 0.0f;
		if (!facingRight)
			Flip();
		CollisionManager cmScript = GetComponent<CollisionManager>();
		animator = GetComponent<Animator> ();
		if (!animator) {
			Debug.LogError("animator can't retrieve");
		}


	}
	public void wakeUp()
	{
		isAwake = true;
		animator.SetBool ("Fly", true);
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(isAwake)
			move ();
	}
	

	
	void move()
	{
		int direction = facingRight ? 1 : -1;
		transform.position = new Vector2(transform.position.x + HorizontalSpeed * Time.fixedDeltaTime * direction,
		                                 transform.position.y + VerticalSpeed * Time.fixedDeltaTime);

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
	// only change scale
	public void Flip() {
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		
	}
}
