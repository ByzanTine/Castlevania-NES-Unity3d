using UnityEngine;
using System.Collections;

public class Globals : MonoBehaviour {

	public static float WhipLengthLong = 0.48f; // 48 pixel
	public static float WhipLengthShort = 0.22f;  // 22 pixel
	public static float PivotToWhipStart = 0.14f; // 14 pixel
	public static float SquatOffset = -0.05f; // 5 pixel 
	public static float StairStepLength = 0.08f; // 8 pixel
	void Awake() {
		Physics2D.raycastsHitTriggers= true;
	}

}
