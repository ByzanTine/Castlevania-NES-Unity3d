using UnityEngine;
using System.Collections;

public class StairManager : MonoBehaviour {
	public enum ON_STAIR_STATE {
		Up,
		Down,
		Not
	};

	public enum ON_STAIR_AREA {
		PrepUp,
		PrepDown,
		onStair, // should be deprecated
		Not
	};
	public ON_STAIR_AREA  onStairArea = ON_STAIR_AREA.Not;
	public ON_STAIR_STATE onStairState = ON_STAIR_STATE.Not;

	//[HideInInspector]
	public int curNumOfSteps = 0;
	public int curStairSteps = 0; // will obtain the value when switching state

	private float upDownStairAnimInterval = 0.33f;
	private PlayerController pc;
	private Animator animator;
	private float prepXcenter; // Used only for preparation state, record the place player need to go 
	private Globals.STAIR_FACING Stairfacing; // true for right, false for left
	private StairController curStair;
	// NOTICE: this stair facing is the parent stair facing
	// Not the facing of the two triggers
	public Globals.STAIR_FACING getCurStairFacing() {
		return curStair.stairFacing;
	}

	void Start () {
		onStairState = ON_STAIR_STATE.Not;
		pc = GetComponent<PlayerController> ();
		animator = GetComponent<Animator> ();
	}

	// return whether player is involved with stair
	public bool isInStairArea () {
		return onStairArea != ON_STAIR_AREA.Not || onStairState != ON_STAIR_STATE.Not;
	}
	// return true if the object is on stair
	public bool isOnStair() {
		return onStairState != ON_STAIR_STATE.Not;
	}

	public bool isWalkingOnStair() {
		return onStairState == ON_STAIR_STATE.Up || onStairState == ON_STAIR_STATE.Down;
	}
	public bool isOnStairAnimationPlaying() {
		return animator.GetBool("UpStair") || animator.GetBool("DownStair");
	}
	// change facing of the character
	public void switchToState(ON_STAIR_STATE state_in) {
		if (onStairState == ON_STAIR_STATE.Not) {
			animator.SetBool("onStair", true);
		}
		else if (state_in == ON_STAIR_STATE.Not) {
			animator.SetBool("onStair", false);
		}
		// UP to down or down to up
		else if (onStairState == ON_STAIR_STATE.Up && state_in == ON_STAIR_STATE.Down) {

			pc.Flip();
		}
		else if (onStairState == ON_STAIR_STATE.Down && state_in == ON_STAIR_STATE.Up) {
			pc.Flip();
		}
		onStairState = state_in;

	}
	// When in prep state, call this function to change the state
	public void switchToState(ON_STAIR_AREA state_in, Globals.STAIR_FACING Stairfacing_in, 
	                          GameObject StairTrigger) {
		prepXcenter = StairTrigger.transform.position.x;
		Stairfacing = Stairfacing_in;
		onStairArea = state_in;

		curStair = StairTrigger.transform.parent.gameObject.GetComponent<StairController>();
		curStairSteps = curStair.StairSteps;
		if (!curStair) {
			Debug.LogError("STAIR: can't get parent stair controller");
		}
		// get the curStairSteps here
		// determin curNumOfSteps here
		if (state_in == ON_STAIR_AREA.PrepDown && onStairState == ON_STAIR_STATE.Not) {
			curNumOfSteps = curStairSteps;
		}
		else if (state_in == ON_STAIR_AREA.PrepUp && onStairState == ON_STAIR_STATE.Not) {
			curNumOfSteps = 0;
		}

	}

	public void tryGoUpStair () {
		if (onStairState == ON_STAIR_STATE.Down || onStairState == ON_STAIR_STATE.Up) {
			// last step on the stair
			if (curStairSteps - curNumOfSteps == 1) {
				if (!isOnStairAnimationPlaying())
					StartCoroutine (GoUpStairToNormal ());
			}
			else {
				if (!isOnStairAnimationPlaying())
					StartCoroutine (GoUpStair ());
			}
			
		}
		else if (onStairState == ON_STAIR_STATE.Not && onStairArea == ON_STAIR_AREA.PrepUp) {
			// NOTICE: interrupt can happen here, say a up button is hit

			if (animator.GetBool("UpStair"))
				Debug.LogError("Animator in impossible state, when in prep up area, but in up animation state");
			// do two thigns, first ran to the center, then Go up stair
			if (!isOnStairAnimationPlaying())
				StartCoroutine(initGoUpStair());

		}


	}
	public void tryGoDownStair () {
		// inside the area of prep_up, could resolve to normal state
		if (onStairState == ON_STAIR_STATE.Down || onStairState == ON_STAIR_STATE.Up) {
			if (curNumOfSteps == 1) { // on the first level on stair
				if (!isOnStairAnimationPlaying())
					StartCoroutine (GoDownStairToNormal ());
			}
			else {
				if (!isOnStairAnimationPlaying())
					StartCoroutine (GoDownStair ());
			}


		}
		else if (onStairState == ON_STAIR_STATE.Not && onStairArea == ON_STAIR_AREA.PrepDown) {

			if (animator.GetBool("DownStair"))
				Debug.LogError("Animator in impossible state, when in prep down area, but in down animation state");
			StartCoroutine (initGoDownStair ());
		}


	}
	
	private IEnumerator GoUpStair() {
		
		animator.SetBool ("UpStair", true);
		switchToState (StairManager.ON_STAIR_STATE.Up);
		// Do Lerp here 
		int facing = pc.facingRight ? 1 : -1;
		Vector2 destination = new Vector2 (
			transform.position.x + facing * Globals.StairStepLength,
			transform.position.y + Globals.StairStepLength
			);
		StartCoroutine(MoveObject (transform.position, destination, upDownStairAnimInterval));
		
		yield return new WaitForSeconds (upDownStairAnimInterval);
		curNumOfSteps++;
		animator.SetBool ("UpStair", false);
	}
	
	private IEnumerator GoDownStair() {
		
		animator.SetBool ("DownStair", true);
		switchToState (StairManager.ON_STAIR_STATE.Down);
		// Do Lerp here 
		int facing = pc.facingRight ? 1 : -1;
		
		Vector2 destination = new Vector2 (
			transform.position.x + facing * Globals.StairStepLength,
			transform.position.y + -1 * Globals.StairStepLength
			);
		StartCoroutine(MoveObject (transform.position, destination, upDownStairAnimInterval));

		yield return new WaitForSeconds (upDownStairAnimInterval);
		curNumOfSteps--;
		animator.SetBool ("DownStair", false);
	}
	// Move object using pure lerp smoothing 
	public IEnumerator MoveObject(Vector2 from, Vector2 to, float time) {
	
		float i = 0.0f;
		float rate = 1.0f / time;
		while (i < 1.0f) {
			i += Time.fixedDeltaTime * rate;
			transform.position = Vector2.Lerp(from, to, i);
			// Debug.Log("Lerp once" + i);
			yield return null;
		}
	}

	private IEnumerator initGoUpStair() {
		// using current speed system to move the object 
		Vector2 destination = new Vector2(prepXcenter, transform.position.y);
		if (transform.position.x < prepXcenter)	
			pc.GetComponent<Animator> ().SetInteger ("Speed", 1);
		else 
			pc.GetComponent<Animator> ().SetInteger ("Speed", -1);
		// determine the animationRatio by the distance from the center
		float animRatio = Mathf.Abs (transform.position.x - prepXcenter) / 
						(Globals.StairUpTriggerColliderWidth / 2 + Globals.playerWidth / 2);
		// Debug.Log ("animRatio: " + animRatio);
		animRatio = Mathf.Clamp (animRatio, 0.01f, 1.0f);

		yield return StartCoroutine(MoveObject(transform.position, destination, animRatio * 1.0f));
		pc.GetComponent<Animator> ().SetInteger ("Speed", 0);

		// Then do a pixel correction 
		transform.position = new Vector2(prepXcenter, transform.position.y);
		// Then correct the object facing
		// small hack to reset the Speed when trying to adjust the facing
		// Don't let the player controller do a fixed update that mess with the facing
		pc.GetComponent<Animator> ().SetInteger ("Speed", 0);
		// DIRTY HACK ABOVE
		adjustFacingToStair();

		if (!isOnStairAnimationPlaying())
			StartCoroutine (GoUpStair());
	}

	private IEnumerator initGoDownStair() {
		// using current speed system to move the object 
		Vector2 destination = new Vector2(prepXcenter, transform.position.y);
		if (transform.position.x < prepXcenter)	
			pc.GetComponent<Animator> ().SetInteger ("Speed", 1);
		else 
			pc.GetComponent<Animator> ().SetInteger ("Speed", -1);
		// determine the animationRatio by the distance from the center
		float animRatio = Mathf.Abs (transform.position.x - prepXcenter) / 
			(Globals.StairUpTriggerColliderWidth / 2 + Globals.playerWidth / 2);
		// Debug.Log ("animRatio: " + animRatio);
		animRatio = Mathf.Clamp (animRatio, 0.01f, 1.0f);
		
		yield return StartCoroutine(MoveObject(transform.position, destination, animRatio * 1.0f));
		pc.GetComponent<Animator> ().SetInteger ("Speed", 0);
		
		// Then do a pixel correction 
		transform.position = new Vector2(prepXcenter, transform.position.y);
		// Then correct the object facing
		// small hack to reset the Speed when trying to adjust the facing
		// Don't let the player controller do a fixed update that mess with the facing
		pc.GetComponent<Animator> ().SetInteger ("Speed", 0);
		// DIRTY HACK ABOVE
		adjustFacingToStair();
		if (!isOnStairAnimationPlaying())
			StartCoroutine (GoDownStair());
	}


	private IEnumerator GoDownStairToNormal() {
		StartCoroutine (GoDownStair ());
		yield return new WaitForSeconds (upDownStairAnimInterval);
		switchToState(ON_STAIR_STATE.Not);
	}

	private IEnumerator GoUpStairToNormal() {
		StartCoroutine (GoUpStair ());
		yield return new WaitForSeconds (upDownStairAnimInterval);
		switchToState(ON_STAIR_STATE.Not);
	}

	private void adjustFacingToStair() {
		if (Stairfacing == Globals.STAIR_FACING.Right && !pc.facingRight) {
			pc.Flip();
		}
		else if (Stairfacing == Globals.STAIR_FACING.Left && pc.facingRight) {
			pc.Flip();
		}
	}

}
