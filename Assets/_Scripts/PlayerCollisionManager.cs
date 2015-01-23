using UnityEngine;
using System.Collections;

public class PlayerCollisionManager : MonoBehaviour {
	
	static private int[] wallStatus = new int[4] {0, 0, 0, 0}; // could be true on 0, 1, 2, 3;
	//corresponding to: RDirection{Right,Left, Top, Bottom};
	//private enum RDirection{Right,Left, Top, Bottom};
	
	public void playerCollisionEnter(int direction)
	{
		wallStatus[direction]++;
		print ("+" + direction);
	}
	
	public void playerCollisionExit(int direction)
	{
		wallStatus[direction]--;
		print ("-" + direction);

	}
	
	public bool isWallOnRight () {
		return wallStatus[0] != 0;
	}
	
	public bool isWallOnLeft () {
		return wallStatus[1] != 0;
	}
	
	public bool isWallOnBottom () {
		return wallStatus[3] != 0;
	}
	
}
