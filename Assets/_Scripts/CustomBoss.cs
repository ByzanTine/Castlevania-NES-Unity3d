using UnityEngine;
using System.Collections;

public class CustomBoss : BossMotion {

	private DragonWakeUpBoss drwake;

	new void Start() {
		base.Start ();
		// do others
		drwake = GetComponent<DragonWakeUpBoss> ();

	}


	public override void wakeUp()
	{
		animator.SetBool ("isAwake", true);
		awake = true;
		StartCoroutine (bossSpeedControl());
		drwake.disable ();

	}	

	protected override IEnumerator onHit()
	{
		hitted = true;
		drwake.enable ();
		int len = 90;
		for (int i = 0; i < len; i++) {
			transform.rotation *= Quaternion.Euler (0, 0, 360.0f/len);
			yield return new WaitForSeconds (5.0f/len);

		}
		drwake.disable ();
		hitted = false;
		collider2D.enabled = true;
	}
}
