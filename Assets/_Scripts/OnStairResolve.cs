using UnityEngine;
using System.Collections;

public class OnStairResolve : MonoBehaviour {


	public Globals.STAIR_FACING stairFacing;

	void OnTriggerEnter2D(Collider2D other) {
		Debug.Log("STAIR: Enter Trigger of Going up stairs");
		GameObject player = other.gameObject;

		StairManager stairMan = player.GetComponent<StairManager>();
		if (!stairMan) {
			Debug.Log("The colliding game object doesn't have stair manager");
			return;
		}

		stairMan.switchToState(StairManager.ON_STAIR_AREA.PrepUp, transform.position.x, stairFacing);

	}

	void OnTriggerExit2D(Collider2D other) {
		Debug.Log("STAIR: Exit Trigger of Going up stairs");
		GameObject player = other.gameObject;
		
		StairManager stairMan = player.GetComponent<StairManager>();
		if (!stairMan) {
			Debug.Log("The colliding game object doesn't have stair manager");
			return;
		}
		if (stairMan.onStairArea == StairManager.ON_STAIR_AREA.PrepUp)
			stairMan.onStairArea = StairManager.ON_STAIR_AREA.Not;
	}
}
