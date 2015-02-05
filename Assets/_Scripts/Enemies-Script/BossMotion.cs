using UnityEngine;
using System.Collections;

public class BossMotion : OnWhipEvent {
	
	public GameObject itemPrefab;
	public int bossHealth = Globals.maxBossHealth;
	protected bool awake = false;

	
	protected bool hitted = false;
	protected Animator animator;
	private Vector2 speed;
	private Transform playerPos;
	private Vector2 initPosition;
	private float horExtent;
	public void Start()
	{
		float verExtent = Camera.main.camera.orthographicSize;
		horExtent = verExtent * Screen.width / Screen.height - 0.15f;
		animator = GetComponent<Animator> ();
		playerPos = GameObject.FindGameObjectWithTag ("Player").transform;
		initPosition = new Vector2 (transform.position.x, transform.position.y);
	}

	void FixedUpdate()
	{
		if(awake)
			move();
	}

	protected void move ()
	{
		Vector3 pos = transform.position;
		pos.x += speed.x;
		pos.y += speed.y;
		if (pos.x < initPosition.x - horExtent + 0.5f * collider2D.bounds.size.x
		    || pos.x > initPosition.x + horExtent - 0.5f * collider2D.bounds.size.x) {
			speed.x *= -1;
		}
		if (pos.y > initPosition.y) {
			speed.y *= 0;
		}

		transform.position = pos;
	}


	protected IEnumerator bossSpeedControl ()
	{
		speed = new Vector2 (-0.01f, -0.003f);
		yield return new WaitForSeconds (2f);
		speed *= 0;
		yield return new WaitForSeconds (1f);

		while(bossHealth >= 0 )
		{
			// Weak attack
			Vector3 moveDir;
			for(int i = 0; i < 7; i++)
			{
				moveDir = playerPos.position - transform.position;
				speed = new Vector2 (moveDir.x/100,moveDir.y/100);
				yield return new WaitForSeconds (0.2f);
			}

			// Go back to top slightly
			speed = new Vector2 (-0.01f, 
			                     (playerPos.position.y + 0.3f * Random.value + 0.2f)/100f);
			yield return new WaitForSeconds (2f);
			speed *= 0;
			yield return new WaitForSeconds (1f);

			// Strong attack
			moveDir = playerPos.position - transform.position;
			for(int i = 1; i <= 2; i++)
			{
				moveDir = playerPos.position - transform.position;
				speed = new Vector2 (moveDir.x/35,moveDir.y/30);
				yield return new WaitForSeconds (0.4f * i);
			}


			// Go back to top
			speed = new Vector2 (-0.005f, 
			                     (playerPos.position.y + 0.3f * Random.value + 0.2f)
			                     * Time.fixedDeltaTime/3f);
			yield return new WaitForSeconds (3f);
			speed *= 0;
			yield return new WaitForSeconds (1f);

			moveDir = playerPos.position - transform.position;
			speed = new Vector2 (
				(-moveDir.x + Random.value/5f - 0.1f)/50f, 0.003f);
			yield return new WaitForSeconds (2f);
			speed *= 0;
			yield return new WaitForSeconds (2f);
		}
	}

	public override void onWhipEnter (){
		if (!awake)
			return;
		if (!hitted) 
		{
			bossHealth -= 2;

			GameObject hitSE = Resources.Load (Globals.SEdir + "hitSE") as GameObject;
			Instantiate (hitSE, transform.position, Quaternion.identity);

			if(bossHealth <= 0)
			{
				StartCoroutine (revealItemAndDestroy());
				return;
			}
			StartCoroutine (onHit());
		}
	}

	protected virtual IEnumerator onHit()
	{
		hitted = true;
		yield return new WaitForSeconds (0.3f);
		hitted = false;
	}

	public virtual void wakeUp()
	{
		animator.SetBool ("isAwake", true);
		awake = true;
		StartCoroutine (bossSpeedControl());
	}
	
	IEnumerator revealItemAndDestroy()
	{
		hitted = true;

		GameObject deathEffect = Resources.Load ("Prefab/death") as GameObject;
		Instantiate (deathEffect, transform.position, Quaternion.identity);

		yield return new WaitForSeconds (0.1f);
		Instantiate (itemPrefab, transform.position, Quaternion.identity);
		
		Destroy (this.gameObject);
		yield return null;
		
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
		PlayerController pcScript = gb.GetComponent<PlayerController> ();
		pcScript.HandleHurt ();
	}
}


