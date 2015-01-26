using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float jumpHeight = 2.0f;
	public float HorizonalSpeedScale; // define in editor
	public float initVerticalSpeed;
	public float StairStepLength; // absolute value.
	public float VerticalAccerlation;

	// [HideInInspector]
	public bool facingRight = false;
	[HideInInspector]
	public float VerticalSpeed;
	// not on stairs curHorizontalVelocity * HorizonalSpeedScale
	// on stairs, a constant value, involving +-
	[HideInInspector]
	public float HorizontalSpeed; 
	public bool grounded;



	private Animator animator;
	private WhipAttackManager whipAttManager;
	private StairManager stairManager;
	private PlayerCollisionManager collManager;
	private int curHorizontalVelocity = 0; // should only have values -1, 0, 1

	public int CurHorizontalVelocity
	{
		get
		{
			//Some other code
			return curHorizontalVelocity;
		}
		set
		{
			//Some other code
			curHorizontalVelocity = value;
		}
	}

	// Use this for initialization
	void Start () {
		// init input manager
		initInputEventHandler ();
		animator = GetComponent<Animator> ();
		whipAttManager = GetComponent<WhipAttackManager> ();
		stairManager = GetComponent<StairManager> ();
		collManager = GetComponent<PlayerCollisionManager> ();
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
		InputManager.Instance.OnKeyPress_Up += HandleOnKeyPress_Up;
		InputManager.Instance.OnKeyUp_Up += HandleOnKeyUp_Up;
		InputManager.Instance.OnKeyPress_Down += HandleOnKeyPress_Down;
	}





	// ============================================================================ //
	/*
	 * Event Handlers 
	 */
	void HandleOnKeyUp_Right ()
	{
		if (!grounded) {
			return;
		}
		if (curHorizontalVelocity != 0) {
			animator.SetInteger("Speed", 0);
		}
	}

	void HandleOnKeyUp_Left ()
	{
		if (!grounded) {
			return;
		}
		if (curHorizontalVelocity != 0) {
			animator.SetInteger("Speed", 0);
		}

	}
	void HandleOnKeyPress_Right () {
		// Debug.Log ("Get Axis Right");
		// reverse as left, depend go up or down by stair facing
		if (stairManager.isOnStair ()) {
			if (stairManager.getCurStairFacing() == Globals.STAIR_FACING.Right)
				stairManager.tryGoUpStair();
			else 
				stairManager.tryGoDownStair();

			return;
		}
		if (!grounded || whipAttManager.attacking)
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
		// Debug.Log ("Get Axis Left");
		// When on stair, go up or down depend on the current stair facing 
		if (stairManager.isOnStair ()) {
			if (stairManager.getCurStairFacing() == Globals.STAIR_FACING.Right)
				stairManager.tryGoDownStair();
			else 
				stairManager.tryGoUpStair();

			return;
		}
		if (!grounded || whipAttManager.attacking )
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

	void HandleOnKeyPress_Up ()
	{
		// Debug.Log("On Key Press: Up");
		if (stairManager.isInStairArea())
			stairManager.tryGoUpStair ();
	}
	
	void HandleOnKeyUp_Up ()
	{
		// Debug.Log("On Key Up: Up");

	}

	void HandleOnKeyDown_A () {
		// Debug.Log ("Key A pressed");
		// jump	
		if (grounded 
		    && !animator.GetBool("Squat")
		    && !animator.GetBool("Jump")
		    && !whipAttManager.attacking)
			StartCoroutine(Jump());


	}

	void HandleOnKeyDown_B () {
		// Debug.Log ("Key B pressed");
		// attack
		// delegate to WhipAttackManager
		StartCoroutine (whipAttManager.WhipAttack());
	}

	
	
	void HandleOnKeyDown_Down () {
		// Debug.Log ("Key Down arrow or S is activated");
		// squat enable
		// TODO enable squat when in prep_up_stair
		if (stairManager.isInStairArea()) {
			stairManager.tryGoDownStair();
		}
		else {
			animator.SetBool ("Squat", true);
		}
		
	}

	void HandleOnKeyPress_Down ()
	{
		// Debug.Log("Get Axis Down");
		// TODO decide if the object is already going down
		if (stairManager.isInStairArea()) {
			stairManager.tryGoDownStair();
		}
	}

	void HandleOnKeyUp_Down () {
		// squat disable 
		animator.SetBool ("Squat", false);
	}

	// ============================================================================ //

	IEnumerator Jump () {
		// jump animation 
		animator.SetBool ("Jump", true);
		grounded = false;

		VerticalSpeed = initVerticalSpeed;
		yield return new WaitForSeconds (0.5f);
		animator.SetBool ("Jump", false);
		 
	}


	// switch the facing to adjust the animation
	void FixedUpdate () {

		if (!stairManager.isOnStair()) {
			normalFixedUpdate();
		}

		// transform facing update
		if (animator.GetInteger("Speed") > 0 && !facingRight)
			Flip();
		if (animator.GetInteger("Speed") < 0 && facingRight)
			Flip();
	}

	// without considering stairs 
	void normalFixedUpdate() {
		// if on Wall, let curVelo 
		
		if (collManager.isWallOn(Globals.Direction.Right)) {

			curHorizontalVelocity = curHorizontalVelocity > 0 ? 0 : curHorizontalVelocity;
		}
		if (collManager.isWallOn(Globals.Direction.Left)) {
			curHorizontalVelocity = curHorizontalVelocity < 0 ? 0 : curHorizontalVelocity;
		}
		// Horizontal Update
		if (collManager.isWallOn(Globals.Direction.Bottom)) {
			if(VerticalSpeed < 0)
			{
				VerticalSpeed = 0;
				
				grounded = true;
				animator.SetInteger("Speed", 0);

//				float reviseHeight = collManager.curBoxTop - collider2D.bounds.min.y;

				// Vertical overwrite update
				transform.position = new Vector2 (
					transform.position.x ,
					collManager.curBoxTop + collider2D.bounds.size.y/2.0f
					);
			}

		}
		else {
			grounded = false;
		}
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



	}


	// Update is called once per frame
	void Update () {
		curHorizontalVelocity = animator.GetInteger ("Speed");

	}





	public void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}
}
