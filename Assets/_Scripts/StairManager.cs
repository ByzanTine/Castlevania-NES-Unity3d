using UnityEngine;
using System.Collections;

public class StairManager : MonoBehaviour {
	public enum ON_STAIR_STATE {
		Up,
		Down,
		Not
	};
	public ON_STAIR_STATE onStairs = ON_STAIR_STATE.Not;
	// Use this for initialization
	private PlayerController pc;
	private Animator animator;
	void Start () {
		onStairs = ON_STAIR_STATE.Not;
		pc = GetComponent<PlayerController> ();
		animator = GetComponent<Animator> ();
	}
	// return true if the object is on stair 
	public bool isOnStair() {
		return onStairs != ON_STAIR_STATE.Not;
	}
	// change facing of the character
	private void switchToState(ON_STAIR_STATE state_in) {
		if (onStairs == ON_STAIR_STATE.Not) {
			animator.SetBool("onStair", true);
		}
		else if (state_in == ON_STAIR_STATE.Not) {
			animator.SetBool("onStair", false);
		}
		else if (onStairs != state_in) {
			// UP to down or down to up
			onStairs = state_in;
			pc.Flip();
		}

	}

	
	public IEnumerator GoUpStair() {
		
		animator.SetBool ("UpStair", true);
		switchToState (StairManager.ON_STAIR_STATE.Up);
		// TODO Do Lerp here 
		int facing = pc.facingRight ? 1 : -1;
		
		transform.position = new Vector2 (
			transform.position.x + facing * Globals.StairStepLength,
			transform.position.y + Globals.StairStepLength
			);
		
		yield return new WaitForSeconds (0.1f);
		
		animator.SetBool ("UpStair", false);
	}
	
	public IEnumerator GoDownStair() {
		
		animator.SetBool ("DownStair", true);
		switchToState (StairManager.ON_STAIR_STATE.Down);
		// TODO Do Lerp here 
		int facing = pc.facingRight ? 1 : -1;
		
		transform.position = new Vector2 (
			transform.position.x + facing * Globals.StairStepLength,
			transform.position.y + -1 * Globals.StairStepLength
			);
		
		yield return new WaitForSeconds (0.1f);
		
		animator.SetBool ("DownStair", false);
	}


}
