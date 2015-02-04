using UnityEngine;
using System.Collections;

public class DragonWakeup : MonoBehaviour {


	public float randomIndex;
	public bool sprial;
	public float Rate; // shoot interval
	public float AngleIncrement = 20.0f;
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			WakeUp ();
		}
	}


	public virtual void WakeUp (){

		ShootFireBall[] lmScript = GetComponentsInChildren<ShootFireBall>();
		
		
		
		foreach (ShootFireBall sfb in lmScript) {
			float wakeupTime = Random.Range(0.0f, randomIndex);
			sfb.wakeUp (wakeupTime, sprial, Rate, AngleIncrement);
			
		}
		
		collider2D.enabled = false;
	}

}
