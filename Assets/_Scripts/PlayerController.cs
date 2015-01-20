using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private Animator animator;
	bool facingRight = false;
	public float jumpHeight = 2.0f;
	public float HorizonalSpeedScale; // define in editor
	public float initVerticalSpeed;
	public bool grounded;

	private int curHorizontalVelocity = 0; // should only have values -1, 0, 1
	private int curVerticalVelocity = 0; 

	public float VerticalSpeed;
	public float VerticalAccerlation;
	// Use this for initialization
	void Start () {
		// init input manager
		initInputEventHandler ();
		animator = GetComponent<Animator> ();
		Flip (); // since the raw sprite face left
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

	}
	// ============================================================================ //
	/*
	 * Event Handlers 
	 */
	void HandleOnKeyUp_Right ()
	{
		if (curHorizontalVelocity != 0) {
			animator.SetInteger("Speed", 0);
		}
	}

	void HandleOnKeyUp_Left ()
	{
		if (curHorizontalVelocity != 0) {
			animator.SetInteger("Speed", 0);
		}

	}
	void HandleOnKeyPress_Right () {
		if (!grounded)
			return;
		if (curHorizontalVelocity == 0) {
			animator.SetInteger("Speed", 1);
		}
		else if (curHorizontalVelocity == 1) {
			;
		}
		else if (curHorizontalVelocity == -1) {
			animator.SetInteger("Speed", 0);
		}
		else {
			Debug.LogError("Speed of a value different to 1,-1,0; Speed: " + curHorizontalVelocity);
		}
	}
	void HandleOnKeyPress_Left () {
		if (!grounded)
			return;
		if (curHorizontalVelocity == 0) {
			animator.SetInteger("Speed", -1);
		}
		else if (curHorizontalVelocity == 1) {
			animator.SetInteger("Speed", 0);
		}
		else if (curHorizontalVelocity == -1) {
			// do nothing
			;
		}
		else {
			Debug.LogError("Speed of a value different to 1,-1,0; Speed: " + curHorizontalVelocity);
		}
	}

	void HandleOnKeyDown_A () {
		Debug.Log ("Key A pressed");
		// jump	
		Vector2 curPos = transform.position;
		if (grounded && !animator.GetBool("Jump"))
			StartCoroutine(Jump());


	}

	// ============================================================================ //

	IEnumerator Jump () {
		// jump animation 
		animator.SetBool ("Jump", true);
		grounded = false;

		VerticalSpeed = initVerticalSpeed;
		// TODO Hardcode
		yield return new WaitForSeconds (1.0f);
		animator.SetBool ("Jump", false);
		 
	}

	void HandleOnKeyDown_B () {
		Debug.Log ("Key B pressed");
		animator.SetInteger ("Attack", 1);
		StartCoroutine (DelayDisableAttack());
		// attack 
	}

	IEnumerator DelayDisableAttack() {
		yield return new WaitForSeconds(0.1f);
		animator.SetInteger ("Attack", 0);

	}

	void HandleOnKeyDown_Down () {
		// Debug.Log ("Key Down arrow or S is activated");
		// squat enable
		animator.SetBool ("Squat", true);

	}

	void HandleOnKeyUp_Down () {
		// squat disable 
		animator.SetBool ("Squat", false);
	}
	// switch the facing to adjust the animation
	void FixedUpdate () {
		// Horizontal Update
		transform.position = new Vector2 (
			transform.position.x + curHorizontalVelocity * HorizonalSpeedScale * Time.fixedDeltaTime,
			transform.position.y
			);
		// Vertical update
		transform.position = new Vector2 (
			transform.position.x ,
			transform.position.y + VerticalSpeed * Time.fixedDeltaTime
			);
		if (!grounded)
			VerticalSpeed += VerticalAccerlation*Time.fixedDeltaTime;
		if (transform.position.y < 0.0f) {
			grounded = true;
			transform.position = new Vector2(transform.position.x, 0.0f);
			VerticalSpeed = 0.0f;
		}
		// transform facing update
		if (animator.GetInteger("Speed") > 0 && !facingRight)
			Flip();
		if (animator.GetInteger("Speed") < 0 && facingRight)
			Flip();


	}


	// Update is called once per frame
	void Update () {
		curHorizontalVelocity = animator.GetInteger ("Speed");

	}





	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}
}
