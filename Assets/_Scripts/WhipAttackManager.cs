using UnityEngine;
using System.Collections;

public class WhipAttackManager : MonoBehaviour {

	public int whipLevel = 1;
	public LayerMask collideLayer;
	[HideInInspector] 
	public bool attacking;
	[HideInInspector] 
	public bool testDamage; // NOTICE: update in animation of all attacks 
	private Animator animator;
	private PlayerController playerControl;

	private string tag_attack = "Attack";
	private float attackWaitInterval = 0.33f;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		playerControl = GetComponent<PlayerController> ();
		attacking = false;
		testDamage = false;
	}
	

	public IEnumerator WhipAttack() {
		// stop if walking 
		if (playerControl.grounded && animator.GetInteger("Speed") != 0) {
			animator.SetInteger("Speed", 0);
		}
		attacking = true;
		animator.SetInteger (tag_attack, whipLevel);

		float SEDelay = 0.15f;

		yield return new WaitForSeconds(SEDelay);

		GameObject whipHitSE = Resources.Load (Globals.SEdir + "whipHitSE") as GameObject;
		Instantiate (whipHitSE, transform.position, Quaternion.identity);

		yield return new WaitForSeconds(attackWaitInterval - SEDelay);
		animator.SetInteger (tag_attack, 0);
		attacking = false;


	}
	public void UpgradWhip () {
		StartCoroutine (UpgradeWhipAnim ());
	}

	IEnumerator UpgradeWhipAnim() {
		whipLevel++;
		Time.timeScale = 0.2f;
		InputManager.Instance.disableControl = true;
		animator.SetInteger("Speed", 0);
		animator.SetBool ("PickUpWhip", true);
		// TODO pause ?
		yield return new WaitForSeconds (0.2f);
		InputManager.Instance.disableControl = false;
		Time.timeScale = 1.0f;
		animator.SetBool ("PickUpWhip", false);

	}
	void FixedUpdate() {
		// for freeze the movement of Simon when attacking
		attacking = (animator.GetInteger (tag_attack) > 0);	

		if (testDamage) {
			Debug.Log("testing damage");
			// collide with other 
			// generate collider or do over raycast
			// there is also a position y drift of squat

			genWhipHit(0.04f);
			genWhipHit(-0.02f);

		}
	}

	void genWhipHit(float yCorrection) {
		Vector3 From = WhipStart(yCorrection);
		Vector3 To = WhipEnd(From);
		// pixel correction +- heightOffset
		genRayHit(From, To);
	}



	void genRayHit (Vector3 From, Vector3 To) {
		RaycastHit2D[] hits = Physics2D.RaycastAll(From, (To-From).normalized, (To-From).magnitude, collideLayer);
		Debug.Log("number of hits: " + hits.Length);
		// Boardcast to all objects that has a WhipEventhandler
		foreach (RaycastHit2D hit in hits) {
			GameObject gb = hit.transform.gameObject;
			OnWhipEvent CC = gb.GetComponent<OnWhipEvent>();	
			if (CC){
				CC.onWhipEnter();
			}
		}
		Debug.DrawLine(From, To, Color.blue, 1.0f);
	}


	Vector3 WhipStart(float yCorrection) {
		return new Vector3(
			transform.position.x + Globals.PivotToWhipStart * (playerControl.facingRight ? 1.0f : -1.0f), 
			transform.position.y + yCorrection + Globals.SquatOffset * (animator.GetBool("Squat") ? 1.0f : 0.0f), 0);
	}

	Vector3 WhipEnd(Vector3 From) {
		if (whipLevel <= 2)
			return new Vector3(
				From.x + Globals.WhipLengthShort * (playerControl.facingRight ? 1.0f : -1.0f), 
				From.y, 0);
		else 
			return new Vector3(
				From.x + Globals.WhipLengthLong * (playerControl.facingRight ? 1.0f : -1.0f), 
				From.y, 0);

	}

}
