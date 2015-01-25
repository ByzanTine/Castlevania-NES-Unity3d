using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	public string targetScene;

	void OnTriggerEnter2D( Collider2D coll ) {
		Debug.Log ("rdy cg sce" + coll.gameObject.tag);
		if (coll.gameObject.tag == "Player") {
			Debug.Log ("cg sce");
			Application.LoadLevel (targetScene);
		}
	}
}
