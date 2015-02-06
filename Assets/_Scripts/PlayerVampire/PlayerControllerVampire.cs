using UnityEngine;
using System.Collections;

public class PlayerControllerVampire : PlayerController {

	public GameObject weapon;
	private hurtVampireMan hurtMan;
	private StatusManager status;
//	private CollisionManager collManager;

	
	// Use this for initialization
	void Start () {
		// init input manager
		initInputEventHandler ();
		hurtMan = GetComponent<hurtVampireMan> ();
		status = GetComponent<StatusManager> ();
		animator = GetComponent<Animator> ();
//		whipAttManager = GetComponent<WhipAttackManager> ();
//		stairManager = GetComponent<StairManager> ();
		collManager = GetComponent<CollisionManager> ();
//		subWeaponManager = GetComponent<SubWeaponManager> ();
		status.changeBGM ();
	}


	void initInputEventHandler () {
		if (InputManager.Instance == null)
			return;
		InputManager.Instance.OnKeyDown_A += HandleOnKeyDown_A;
		InputManager.Instance.OnKeyDown_B += HandleOnKeyDown_B;
		InputManager.Instance.OnKeyDown_Down += HandleOnKeyDown_Down;
		InputManager.Instance.OnKeyUp_Down += HandleOnKeyUp_Down;
		InputManager.Instance.OnKeyPress_Left += HandleOnKeyPress_Left;
		InputManager.Instance.OnKeyPress_Right += HandleOnKeyPress_Right;
		InputManager.Instance.OnKeyUp_Left += HandleOnKeyUp_Left;
		InputManager.Instance.OnKeyUp_Right += HandleOnKeyUp_Right;
		InputManager.Instance.OnKeyPress_Up += HandleOnKeyPress_Up;
		InputManager.Instance.OnKeyUp_Up += HandleOnKeyUp_Up;
		InputManager.Instance.OnKeyPress_Down += HandleOnKeyPress_Down;
		InputManager.Instance.OnKeyDown_Up_And_B += HandleOnKeyPress_Up_And_B;

	}
	// ============================================================================ //
	/*
	 * Event Handlers 
	 */
	void HandleOnKeyUp_Right ()
	{

		if (CurHorizontalVelocity != 0) {
			CurHorizontalVelocity = 0;
		}
	}
	
	void HandleOnKeyUp_Left ()
	{

		if (CurHorizontalVelocity != 0) {
			CurHorizontalVelocity = 0;
		}
		
	}
	void HandleOnKeyPress_Right () {
		// Debug.Log ("Get Axis Right");
		// reverse as left, depend go up or down by stair facing
		CurHorizontalVelocity = 1;
		
		
		
	}
	void HandleOnKeyPress_Left () {
		// Debug.Log ("Get Axis Left");
		// When on stair, go up or down depend on the current stair facing 
		CurHorizontalVelocity = -1;
		
		
		
	}
	
	void HandleOnKeyPress_Up ()
	{
		// Debug.Log("On Key Press: Up");
		VerticalSpeed = 1;
	}
	
	void HandleOnKeyUp_Up ()
	{
		// Debug.Log("On Key Up: Up");
		VerticalSpeed = 0;
	}
	
	void HandleOnKeyDown_A () {
		// Debug.Log ("Key A pressed");
	
		StartCoroutine (Throw ());
		
	}
	
	void HandleOnKeyDown_B () {
		// Debug.Log ("Key B pressed");
		// attack
		// delegate to WhipAttackManager
		GameObject gb = Instantiate (weapon, transform.position, Quaternion.identity) as GameObject;
		float shootSpeed = 2.0f;

		if(!facingRight) {
			shootSpeed *= -1;
			gb.transform.localScale = new Vector3(gb.transform.localScale.x * -1, 
			                                      gb.transform.localScale.y,
			                                      gb.transform.localScale.z);
		}

		gb.rigidbody2D.velocity = new Vector2 (shootSpeed, 0);
	}
	
	
	
	void HandleOnKeyDown_Down () {
		// Debug.Log ("Key Down arrow or S is activated");
		// squat enable
		// TODO enable squat when in prep_up_stair
		VerticalSpeed = -1.0f;
		
	}
	
	void HandleOnKeyPress_Down ()
	{
		// Debug.Log("Get Axis Down");
		VerticalSpeed = -1.0f;
	}
	
	void HandleOnKeyUp_Down () {
		// squat disable 
		VerticalSpeed = 0.0f;
	}
	
	void HandleOnKeyPress_Up_And_B () {
		Debug.Log("INPUT: up and B pressed as chord");

		// do what 

	}

	IEnumerator Throw() {

		if (status.heartNum >= 10) {

			GameObject laughSE = Resources.Load (Globals.SEdir + "laughSE") as GameObject;
			Instantiate (laughSE, transform.position, Quaternion.identity);

			animator.SetBool ("Throw", true);
			GameObject.FindGameObjectWithTag ("Input").GetComponent<InputManager> ().disableControl = true;
			VerticalSpeed = 0.0f;
			CurHorizontalVelocity = 0;
			yield return new WaitForSeconds (1.0f);
			int batNum = 10;
			for (int i = 0; i < batNum; i++) {

//				float randomX = Random.Range(-1.0f, 1.0f);
//				float randomY = Random.Range(-1.0f, 1.0f);
				float degreeDelta = 2 * 3.14f * i / batNum;
				yield return new WaitForSeconds (0.05f);
				status.heartNum -= 1;

				GameObject gb = Instantiate (weapon, transform.position, Quaternion.identity) as GameObject;
				gb.rigidbody2D.velocity = new Vector2 (Mathf.Sin(degreeDelta), Mathf.Cos(degreeDelta)).normalized;
				gb.rigidbody2D.velocity *= 1.8f;
			}

			animator.SetBool ("Throw", false);
			GameObject.FindGameObjectWithTag ("Input").GetComponent<InputManager> ().disableControl = false;
		}

	}

	public override void HandleHurt () {
		if (!hurtMan.Hurting) 
			StartCoroutine(hurtMan.Hurt());

	}
	// ============================================================ //
	void FixedUpdate () {

		if (collManager.isWallOn(Globals.Direction.Right)) {
			
			CurHorizontalVelocity = CurHorizontalVelocity > 0 ? 0 : CurHorizontalVelocity;
		}
		if (collManager.isWallOn(Globals.Direction.Left)) {
			CurHorizontalVelocity = CurHorizontalVelocity < 0 ? 0 : CurHorizontalVelocity;
		}
		// Horizontal Update
		if (collManager.isWallOn (Globals.Direction.Bottom)) {
						if (VerticalSpeed < 0) {
								VerticalSpeed = 0;
								// Vertical overwrite update
								transform.position = new Vector2 (
					transform.position.x,
					collManager.curBoxTop + collider2D.bounds.size.y / 2.0f
								);
						}
				} 
		else {

//			VerticalSpeed -= 0.2f;
		}

		if (CurHorizontalVelocity > 0 && !facingRight)
			facingRight = !facingRight;
		if (CurHorizontalVelocity < 0 && facingRight)
			facingRight = !facingRight;

		float horizontalSpeedFix = 0.7f;

		if(animator.GetBool ("Throw"))
		{
			VerticalSpeed = 0;
			CurHorizontalVelocity = 0;
		}

		transform.position = new Vector2 (
			transform.position.x + horizontalSpeedFix * CurHorizontalVelocity * Time.fixedDeltaTime,
			transform.position.y
			);

		// Vertical update
		transform.position = new Vector2 (
			transform.position.x ,
			transform.position.y + VerticalSpeed * Time.fixedDeltaTime
			);
	}

	void Update () {

	}


}
