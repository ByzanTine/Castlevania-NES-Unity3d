using UnityEngine;
using System.Collections;

public class OnStairResolveDown : MonoBehaviour {


	private Globals.STAIR_FACING stairFacing;

	// init with the stair steps
	void Start() {
		stairFacing = transform.GetComponentInParent<StairController> ().stairFacing == Globals.STAIR_FACING.Left ? 
			Globals.STAIR_FACING.Right : Globals.STAIR_FACING.Left;
	}
	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("STAIR: Enter Trigger of Going up stairs");
		GameObject player = other.gameObject;

		StairManager stairMan = player.GetComponent<StairManager>();
		if (!stairMan) {
			Debug.Log("The colliding game object doesn't have stair manager");
			return;
		}

		stairMan.switchToState(StairManager.ON_STAIR_AREA.PrepDown, 
		                       stairFacing, gameObject);

	}

	void OnTriggerExit2D(Collider2D other) {
		Debug.Log("STAIR: Exit Trigger of Going up stairs");
		GameObject player = other.gameObject;
		
		StairManager stairMan = player.GetComponent<StairManager>();
		if (!stairMan) {
			Debug.Log("The colliding game object doesn't have stair manager");
			return;
		}
		if (stairMan.onStairArea == StairManager.ON_STAIR_AREA.PrepDown)
			stairMan.onStairArea = StairManager.ON_STAIR_AREA.Not;
	}
}
