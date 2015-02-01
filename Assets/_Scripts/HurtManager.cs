using UnityEngine;
using System.Collections;

public class HurtManager : MonoBehaviour {

	public float InvisibleInterval = 1.0f;
	public float initHurtVericalSpeed = 1.0f;

	private bool hurting; // only provide accessor
	public bool Hurting {
		get {
			return hurting;
		}
	}

	private Animator animator;
	private PlayerController pc;
	private SpriteRenderer sprite;
	private StatusManager status;


	void Start () {
		animator = GetComponent<Animator> ();
		pc = GetComponent<PlayerController> ();
		sprite = GetComponent<SpriteRenderer> ();
		status = GetComponent<StatusManager> ();
		hurting = false;
	}
	// ============================================================================ //
	public IEnumerator Hurt () {

		status.playerHealth -= 2;

		hurting = true;

		// TODO do what ever is needed to turn off some collision
		// CODE HERE


		animator.SetBool ("Hurt", true);
		pc.VerticalSpeed += initHurtVericalSpeed;
		// add horizontal speed according to facing
		pc.CurHorizontalVelocity = pc.facingRight? -1 : 1;
		
		yield return new WaitForSeconds (0.33f);
		animator.SetBool ("Hurt", false);
		StartCoroutine (turnInvisible ());	
		// turn 
		animator.SetBool ("Squat", true);
		
		yield return new WaitForSeconds (0.33f);
		pc.CurHorizontalVelocity = 0;
		animator.SetBool ("Squat", false);

		
	}
	
	IEnumerator turnInvisible () {

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
