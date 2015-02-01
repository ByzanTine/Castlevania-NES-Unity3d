using UnityEngine;
using System.Collections;

public class SubWeaponManager : MonoBehaviour {



	[HideInInspector] 
	public bool throwing;

	private Animator animator;
	private PlayerController playerControl;
	private int numWeapons = 3; 
	public GameObject[] subWeapons; // fixed size 
	private float throwWaitInterval = 0.33f;
	private float throwCD = 0.67f;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		playerControl = GetComponent<PlayerController> ();
		throwing = false;
		subWeapons = new GameObject[numWeapons];
		// obtain all prefabs first 
		subWeapons = Resources.LoadAll<GameObject> ("Prefab/SubWeapon") as GameObject[];

	}

	public IEnumerator Throw() {
		// stop if walking 
		if (playerControl.grounded && animator.GetInteger("Speed") != 0) {
			animator.SetInteger("Speed", 0);
		}
		animator.SetBool ("Throw", true);
		throwing = true;	

		// generate whatever 
		GameObject thrown = Instantiate (subWeapons [0], transform.position, Quaternion.identity) as GameObject;
		thrown.transform.localScale = transform.localScale;

		yield return new WaitForSeconds(throwWaitInterval);
		animator.SetBool ("Throw", false);

		yield return new WaitForSeconds(throwCD);
		throwing = false;	



	}

//	void FixedUpdate() {
//		// for freeze the movement of Simon when attacking
//		throwing = animator.GetBool ("Throw");	
//		
//
//	}
	

}
