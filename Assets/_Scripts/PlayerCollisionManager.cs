using UnityEngine;
using System.Collections;

public class PlayerCollisionManager : MonoBehaviour {
	

	//corresponding to: RDirection{Right,Left, Top, Bottom};
	public int[] wallStatus = new int[4] {0, 0, 0, 0}; // could be true on 0, 1, 2, 3;
	public float curBoxTop = -1.0f;
	public void playerCollisionEnter(int direction, float boxTop)
	{
		wallStatus[direction]++;

		// add ground
		if (direction == 3 && boxTop >= curBoxTop) 
		{
			curBoxTop = boxTop;	
		}
	}
	
	public void playerCollisionExit(int direction)
	{
		wallStatus[direction]--;

		// remove ground
		if (direction == 3 && wallStatus[3] == 0) 
		{
			curBoxTop = -1.0f;	
		}
	}

	// functions indicates that whether there is wall on each direction
	public bool isWallOn(Globals.Direction dir) {
		return wallStatus[(int)dir] != 0;
	}
	
}
