using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private Animator animator;
	bool facingRight = false;
	public float jumpHeight = 2.0f;
	public bool grounded;
	private int curVelocity = 0; // should only have values -1, 0, 1
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
		if (curVelocity != 0) {
			animator.SetInteger("Speed", 0);
		}
	}

	void HandleOnKeyUp_Left ()
	{
		if (curVelocity != 0) {
			animator.SetInteger("Speed", 0);
		}

	}
	void HandleOnKeyPress_Right () {
		if (curVelocity == 0) {
			animator.SetInteger("Speed", 1);
		}
		else if (curVelocity == 1) {
			;
		}
		else if (curVelocity == -1) {
			animator.SetInteger("Speed", 0);
		}
		else {
			Debug.LogError("Speed of a value different to 1,-1,0; Speed: " + curVelocity);
		}
	}
	void HandleOnKeyPress_Left () {
		if (curVelocity == 0) {
			animator.SetInteger("Speed", -1);
		}
		else if (curVelocity == 1) {
			animator.SetInteger("Speed", 0);
		}
		else if (curVelocity == -1) {
			// do nothing
			;
		}
		else {
			Debug.LogError("Speed of a value different to 1,-1,0; Speed: " + curVelocity);
		}
	}

	void HandleOnKeyDown_A () {
		Debug.Log ("Key A pressed");
		// jump	
		Vector2 curPos = transform.position;
		Debug.Log (curPos);
		if (!grounded && !animator.GetBool("Jump"))
			StartCoroutine(Jump());


	}

	// ============================================================================ //

	IEnumerator Jump () {
		// jump animation 
		animator.SetBool ("Jump", true);
		Vector2 originPos = transform.position;
		Vector2 upperPos = new Vector2(transform.position.x, transform.position.y + jumpHeight);
		float increment = 0.3f;
		float decay = 0.9f;
		for (float i = 0; i < 1; i+= increment ) {
			// TODO there should be a interrupt when hurt
			yield return new WaitForSeconds(Time.fixedDeltaTime);
			transform.position = Vector2.Lerp (transform.position, upperPos, i);
			increment = increment * decay;
		}
		for (float i = 0; i < 1; i+= 0.1f) {
			// TODO there should be a interrupt when hurt
			yield return new WaitForSeconds(Time.fixedDeltaTime);
			transform.position = Vector2.Lerp (transform.position, originPos, i);
		}
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
		Vector2 afterPos = transform.position;

		if (animator.GetInteger("Speed") > 0 && !facingRight)
			Flip();
		if (animator.GetInteger("Speed") < 0 && facingRight)
			Flip();

	}
	// Update is called once per frame
	void Update () {
		curVelocity = animator.GetInteger ("Speed");

	}



	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}
}
