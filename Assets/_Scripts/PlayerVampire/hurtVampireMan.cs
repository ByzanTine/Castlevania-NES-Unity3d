using UnityEngine;
using System.Collections;

public class hurtVampireMan : MonoBehaviour {

	public float InvisibleInterval = 1.0f;
	public float initHurtVericalSpeed = 1.0f;
	
	private bool hurting; // only provide accessor
	public bool Hurting {
		get {
			return hurting || disableControl;
		}
	}
	
	private bool disableControl;
	
	private Animator animator;
	private PlayerController pc;
	private SpriteRenderer sprite;
	private StatusManager status;
	private StairManager stairMan;
	
	void Start () {
		animator = GetComponent<Animator> ();
		pc = GetComponent<PlayerController> ();
		sprite = GetComponent<SpriteRenderer> ();
		status = GetComponent<StatusManager> ();
		stairMan = GetComponent<StairManager> ();
		hurting = false;
		
		disableControl = false;
	}
	// HACK
	public bool onFlyHurting () {
		return disableControl;
	}
	// ============================================================================ //
	public IEnumerator Hurt () {
		status.playerHealth -= 1;

		hurting = true;
		StartCoroutine (turnInvisible ());	
		yield return null;
	}
	
	public IEnumerator turnInvisible () {
		hurting = true;
		Color curColor = sprite.color;
		curColor.a = curColor.a/2; // turn to half alpha
		sprite.color = curColor;
		
		yield return new WaitForSeconds (InvisibleInterval);
		curColor.a = 1.0f; // return to full alpha
		sprite.color = curColor;
		hurting = false;
		
		// TODO enable physics again 
	}
}
