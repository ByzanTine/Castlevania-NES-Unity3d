using UnityEngine;
using System.Collections;

public class DragonWakeUpBoss : DragonWakeup {

	private ShootFireBall[] lmScript;
	void Start () {
		lmScript = GetComponentsInChildren<ShootFireBall>();
	}

	void OnTriggerEnter2D(Collider2D other){

	}

	public void enable(){

			
		
		Debug.Log("BOSS: enable four way shooting again");	
		foreach (ShootFireBall sfb in lmScript) {
			sfb.transform.gameObject.SetActive(true);
			
		}
		base.WakeUp ();
	}

	public void disable() {

		
		
		
		foreach (ShootFireBall sfb in lmScript) {
			sfb.CancelInvoke();
			sfb.transform.gameObject.SetActive(false);
			
		}
	}
}
