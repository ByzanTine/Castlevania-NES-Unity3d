using UnityEngine;
using System.Collections;

public class WhipAttackManager : MonoBehaviour {

	public int whipLevel = 1;
	[HideInInspector] 
	public bool attacking;
	private Animator animator;

	// legacy - ready to remove
	private string tag_squat_attack = "Attack_Squat";
	private string tag_idle_attack = "Attack_Idle";

	private string tag_attack = "Attack";
	private float attackWaitInterval = 0.5f;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		attacking = false;
	}
	

	public IEnumerator WhipAttack() {

		animator.SetInteger (tag_attack, whipLevel);
		yield return new WaitForSeconds(attackWaitInterval);
		animator.SetInteger (tag_attack, 0);

		
	}
	void FixedUpdate() {
		// for freeze the movement of Simon when attacking
		attacking = (animator.GetInteger (tag_attack) > 0);
	}

}
