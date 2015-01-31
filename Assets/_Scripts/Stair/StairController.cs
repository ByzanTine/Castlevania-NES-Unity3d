using UnityEngine;
using System.Collections;
/// <summary>
/// Stair controller generate the two triggers by the fiels specified by 
/// this script
/// NOTICE: This script generate the colliders on the fly
/// </summary>
public class StairController : MonoBehaviour {

	public int StairSteps; // define in inspector
	public Globals.STAIR_FACING stairFacing;
	public GameObject UpStairTrigger;
	public GameObject DownStairTrigger;

	void Start() {
		generateUpDownTrigger ();
	}

	void generateUpDownTrigger() {
		GameObject Upgb = Instantiate (UpStairTrigger, transform.position, Quaternion.identity) as GameObject;
		Upgb.transform.parent = transform;
		int facing = stairFacing == Globals.STAIR_FACING.Right ? 1 : -1;
		Vector2 downTriggerPos = new Vector2 (transform.position.x + facing * StairSteps * Globals.StairStepLength, 
		                                      transform.position.y + StairSteps * Globals.StairStepLength);
		GameObject Downgb = Instantiate (DownStairTrigger, downTriggerPos, Quaternion.identity) as GameObject;
		Downgb.transform.parent = transform;

	}
}
