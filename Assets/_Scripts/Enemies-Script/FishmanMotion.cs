using UnityEngine;
using System.Collections;

public class FishmanMotion : MonoBehaviour {

	private Vector2 defaultSpeed = new Vector2 (-0.004f, -0.005f);
	private float jumpSpeed = 0.018f;
	private float cannonCD = 3.0f;
	private const float perishInSec = 1.0f;
	
	private Vector2 speed;
	private bool isMoveLeft = true;

	private bool isJumping;
	private bool isShooting;
	private Animator animator;
	private float initVertPos;
	private bool fishMerging = false;

	public GameObject cannonBallPrefab;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		initWave ();

		initVertPos = transform.position.y;
		speed = new Vector2(0.0f, jumpSpeed + defaultSpeed.y);

		GameObject playerObj = GameObject.FindGameObjectWithTag (Globals.playerTag);

		if(playerObj.transform.position.x > transform.position.x)
		{
			isMoveLeft = false;
			Flip();
		}
		if (!isMoveLeft)
			defaultSpeed.x *= -1;

		isJumping = true;
		isShooting = false;
		StartCoroutine (jumping());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		move ();

		if (transform.position.y < initVertPos && !fishMerging) 
		{
			fishMerging = true;
			initWave();
			Destroy(this.gameObject);
		}
	}

	void initWave()
	{
		Debug.Log ("playing fish out");
		GameObject deathEffect = Resources.Load ("Prefab/fishOut") as GameObject;
		Vector3 offset = new Vector3 (0.0f, 0.2f, 0.0f);
		Instantiate (deathEffect, collider2D.bounds.center + offset, Quaternion.identity);
	}

	IEnumerator shooting()
	{
		while(!isJumping)
		{
			yield return new WaitForSeconds(cannonCD);
			if(speed.y == 0)
			{
				isShooting = true;
				animator.SetBool("isShooting", true);

				GameObject cannonBall = Instantiate (cannonBallPrefab, transform.position,Quaternion.identity)
					as GameObject;
				Vector3 pos = cannonBall.transform.position; 

				// adjust y height
				pos.y += collider2D.bounds.size.y/4f;

				cannonBall.transform.position = pos;

				if(!isMoveLeft)
				{
					CannonMotion cmScript = cannonBall.GetComponent<CannonMotion>();
					cmScript.speed *= -1;
				}
				speed.x = 0;
				yield return new WaitForSeconds(0.5f);
				isShooting = false;
				animator.SetBool("isShooting", false);
			}
		}
	}

	IEnumerator jumping()
	{
		for(int i=0; i < 15; ++i)
		{
			yield return new WaitForSeconds(0.1f);
			jumpSpeed /= 1.05f; 
			speed.y = jumpSpeed;
		}
		speed.y = 0;
		yield return new WaitForSeconds(0.3f);

		isJumping = false;
		StartCoroutine (shooting());
	}


	void move()
	{
		Vector3 pos = this.transform.position;
		pos.x += speed.x;
		pos.y += speed.y;
		this.transform.position = pos;
		
		CollisionManager cmScript = GetComponent<CollisionManager>();
		if (cmScript != null && !isJumping) 
		{
			if(cmScript.isWallOn(Globals.Direction.Bottom))
			{
				defaultSpeed.y = -0.015f;
				speed.y = 0;
				speed.x = defaultSpeed.x;
				if(isShooting)
					speed.x = 0;
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
		GameObject deathEffect = Resources.Load ("Prefab/death") as GameObject;
		Instantiate (deathEffect, collider2D.bounds.center, Quaternion.identity);
		Destroy (this.gameObject);
	}
	
	void onPlayerEnter(GameObject gb)
	{
		Debug.Log ("Player hitted");
		PlayerController pcScript = gb.GetComponent<PlayerController> ();
		pcScript.HandleHurt ();

	}

	public void Flip() {
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		
	}
}
