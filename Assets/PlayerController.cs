using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	private Animator animator;
	bool facingRight = false;
	public float jumpHeight = 2.0f;
	private bool grounded;
	// Use this for initialization
	void Start () {
		// init input manager
		initInputEventHandler ();
		animator = GetComponent<Animator> ();
		Flip (); // since the raw sprite face left
	}
	void initInputEventHandler () {
		InputManager.Instance.OnKeyDown_A += OnKeyDown_A;
		InputManager.Instance.OnKeyDown_B += OnKeyDown_B;
		InputManager.Instance.OnKeyDown_Down += OnKeyDown_Down;
		InputManager.Instance.OnKeyUp_Down += OnKeyUp_Down;

	}

	void OnKeyDown_A () {
		Debug.Log ("Key A pressed");
		// jump	

		// jump animation 

	}

	void OnKeyDown_B () {
		Debug.Log ("Key B pressed");
		// shoot 
	}

	void OnKeyDown_Down () {
		// Debug.Log ("Key Down arrow or S is activated");
		// squat enable
		animator.SetBool ("Squat", true);

	}

	void OnKeyUp_Down () {
		// squat disable 
		animator.SetBool ("Squat", false);
	}
	// switch the facing to adjust the animation
	void FixedUpdate () {
		if (animator.GetInteger("Speed") > 0 && !facingRight)
			Flip();
		if (animator.GetInteger("Speed") < 0 && facingRight)
			Flip();

	}
	// Update is called once per frame
	void Update () {
		int velo = animator.GetInteger ("Speed");


		if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightArrow)) {
			animator.SetInteger("Speed", 0);
		}
		else if (velo == 0 && Input.GetKey (KeyCode.RightArrow)) {
			// set anim to have velocity
			animator.SetInteger("Speed", 1);
		}
		else if (velo == -1 && Input.GetKey (KeyCode.RightArrow)) {
			animator.SetInteger("Speed", 0);
		}
		else if (velo == 0 && Input.GetKey (KeyCode.LeftArrow)) {
			animator.SetInteger("Speed", -1);
		}
		else if (velo == 1 && Input.GetKey (KeyCode.LeftArrow)) {
			animator.SetInteger("Speed", 0);
		}



	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}
}
