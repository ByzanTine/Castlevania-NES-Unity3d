using UnityEngine;
using System.Collections;

public class SubWeaponManager : MonoBehaviour {



	[HideInInspector] 
	public bool throwing;

	private Animator animator;
	private PlayerController playerControl;
	private StatusManager status;
	private int numWeapons = 3; 
	public GameObject[] subWeapons; // fixed size 
	private float throwDelay = 0.2f;
	private float throwWaitInterval = 0.33f;
	private float throwCD = 0.67f;

	public bool isCarrying;
	private int weaponId;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		playerControl = GetComponent<PlayerController> ();
		status = GetComponent<StatusManager> ();
		throwing = false;
		subWeapons = new GameObject[numWeapons];
		// obtain all prefabs first 
		subWeapons = Resources.LoadAll<GameObject> ("Prefab/SubWeapon") as GameObject[];

		isCarrying = false;
		weaponId = -1;
	}

	public GameObject getSubWeaponObject () {
		if (weaponId == -1)
			return null;
		else 
			return subWeapons[weaponId];
	}

	public void weaponPickedUp(Globals.SubWeapon weaponId_in)
	{
		isCarrying = true;
		Debug.Log ("subweapon Loaded:" + weaponId_in.ToString ());
		weaponId = (int)weaponId_in;
		if(weaponId >= subWeapons.Length)
			Debug.LogError("weapon not found");

		// TODO
		// update GUI
		// 

	}

	public IEnumerator Throw() {
		if(isCarrying && status.heartNum > 0)
		{

			// stop if walking 
			if (playerControl.grounded && animator.GetInteger("Speed") != 0) {
				animator.SetInteger("Speed", 0);
			}
			animator.SetBool ("Throw", true);
			throwing = true;	
			status.heartNum -= weaponId == (int)Globals.SubWeapon.StopWatch ? 5 : 1;
			yield return new WaitForSeconds(throwDelay);

			GameObject subSE = Resources.Load (Globals.SEdir + "subSE") as GameObject;
			Instantiate (subSE, transform.position, Quaternion.identity);

			// generate whatever 
			GameObject thrown = Instantiate (subWeapons [weaponId], transform.position, Quaternion.identity) as GameObject;
			thrown.transform.localScale = new Vector3 (-1 * transform.localScale.x, 
					                                    transform.localScale.y,
					                                    transform.localScale.z);
			yield return new WaitForSeconds(throwWaitInterval - throwDelay);
			animator.SetBool ("Throw", false);

			yield return new WaitForSeconds(throwCD);
			throwing = false;	
		}
	}

//	void FixedUpdate() {
//		// for freeze the movement of Simon when attacking
//		throwing = animator.GetBool ("Throw");	
//		
//
//	}
	

}
