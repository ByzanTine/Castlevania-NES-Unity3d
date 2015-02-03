using UnityEngine;
using System.Collections;

public class HolyFireController : MonoBehaviour {

	private float damageDelay = 0.3f;

	void Start()
	{
		StartCoroutine (damageCD());
	}

	void OnTriggerEnter2D( Collider2D coll ) {
		GameObject collidedObj = coll.gameObject;
		OnWhipEvent whipScript = collidedObj.GetComponent<OnWhipEvent>();

		if(whipScript != null)
		{
			whipScript.onWhipEnter();
		}
	}

	IEnumerator damageCD()
	{
		yield return new WaitForSeconds(damageDelay);
		collider2D.enabled = false;
		yield return new WaitForSeconds(0.01f);
		collider2D.enabled = true;
	}
}
