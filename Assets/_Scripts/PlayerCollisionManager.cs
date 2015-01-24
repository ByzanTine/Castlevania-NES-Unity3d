using UnityEngine;
using System.Collections;

public class PlayerCollisionManager : MonoBehaviour {
	

	//corresponding to: RDirection{Right,Left, Top, Bottom};
	static private int[] wallStatus = new int[4] {0, 0, 0, 0}; // could be true on 0, 1, 2, 3;
	public void playerCollisionEnter(int direction)
	{
		wallStatus[direction]++;
	}
	
	public void playerCollisionExit(int direction)
	{
		wallStatus[direction]--;
	}

	// functions indicates that whether there is wall on each direction
	public bool isWallOn(Globals.Direction dir) {
		return wallStatus[(int)dir] != 0;
	}
//	
//	public bool isWallOnLeft () {
//		return wallStatus[1] != 0;
//	}
//
//	public bool isWallOnTop () {
//		return wallStatus[2] != 0;
//	}
//	
//	public bool isWallOnBottom () {
//		return wallStatus[3] != 0;
//	}
	
}
