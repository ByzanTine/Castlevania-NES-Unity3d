using UnityEngine;
using System.Collections;

public class PlayerControllerVampire : PlayerController {

	public GameObject weapon;


	
	
	private hurtVampireMan hurtMan;
	private StatusManager status;
	
	// Use this for initialization
	void Start () {
		// init input manager
		initInputEventHandler ();
		hurtMan = GetComponent<hurtVampireMan> ();
		status = GetComponent<StatusManager> ();
		animator = GetComponent<Animator> ();
//		whipAttManager = GetComponent<WhipAttackManager> ();
//		stairManager = GetComponent<StairManager> ();
//		collManager = GetComponent<CollisionManager> ();
//		subWeaponManager = GetComponent<SubWeaponManager> ();

	}


	void initInputEventHandler () {
		InputManager.Instance.OnKeyDown_A += HandleOnKeyDown_A;
		InputManager.Instance.OnKeyDown_B += HandleOnKeyDown_B;
		InputManager.Instance.OnKeyDown_Down += HandleOnKeyDown_Down;
		InputManager.Instance.OnKeyUp_Down += HandleOnKeyUp_Down;
		InputManager.Instance.OnKeyPress_Left += HandleOnKeyPress_Left;
		InputManager.Instance.OnKeyPress_Right += HandleOnKeyPress_Right;
		InputManager.Instance.OnKeyUp_Left += HandleOnKeyUp_Left;
		InputManager.Instance.OnKeyUp_Right += HandleOnKeyUp_Right;;
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
		gb.rigidbody2D.velocity = new Vector2 (1.0f, 0);
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
			animator.SetBool ("Throw", true);
			GameObject.FindGameObjectWithTag ("Input").GetComponent<InputManager> ().disableControl = true;
			VerticalSpeed = 0.0f;
			CurHorizontalVelocity = 0;
			yield return new WaitForSeconds (1.0f);
			for (int i = 0; i < 12; i++) {
				float randomX = Random.Range(-1.0f, 1.0f);
				float randomY = Random.Range(-1.0f, 1.0f);
				GameObject gb = Instantiate (weapon, transform.position, Quaternion.identity) as GameObject;
				gb.rigidbody2D.velocity = new Vector2 (randomX, randomY).normalized;
				
			}
			status.heartNum -= 10;

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
		transform.position = new Vector2 (
			transform.position.x + CurHorizontalVelocity * Time.fixedDeltaTime,
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
