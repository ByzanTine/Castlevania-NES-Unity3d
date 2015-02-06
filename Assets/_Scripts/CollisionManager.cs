using UnityEngine;
using System.Collections;

public class CollisionManager : MonoBehaviour {
	

	//corresponding to: RDirection{Right,Left, Top, Bottom};
	public int[] wallStatus = new int[4] {0, 0, 0, 0}; // could be true on 0, 1, 2, 3;
	public float curBoxTop = -3.0f;

	public delegate void OnTrigger();

	public event OnTrigger ExitGround;
	public event OnTrigger EnterGround;


	public void playerCollisionEnter(int direction, float boxTop)
	{
		wallStatus[direction]++;

		// add ground
		if (direction == 3 && boxTop >= curBoxTop) 
		{
			if(EnterGround != null)
				EnterGround();
//			if(tag.Equals(Globals.playerTag))
//			{
//				Animator animator = GetComponent<Animator>();
//
//				if(!animator.GetBool("Jump"))
//				{
//					GameObject dropSE = Resources.Load (Globals.SEdir + "dropSE") as GameObject;
//					Instantiate (dropSE, transform.position, Quaternion.identity);
//				}
//
//			}
			curBoxTop = boxTop;	
		}
	}

	public void reset()
	{
		wallStatus = new int[4] {0, 0, 0, 0};
	}

	public void playerCollisionExit(int direction)
	{
		wallStatus[direction]--;

		// remove ground
		if (direction == 3 && wallStatus[3] == 0) 
		{
			if(ExitGround != null)
				ExitGround();
			curBoxTop = -3.0f;	
		}
	}

	// functions indicates that whether there is wall on each direction
	public bool isWallOn(Globals.Direction dir) {
		return wallStatus[(int)dir] != 0;
	}

	// functions
	
}
