using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	// both var must be initialized in inspector
	public string targetScene;
	public Vector3 newPos;

	void OnTriggerEnter2D( Collider2D coll ) {
		GameObject collObj = coll.gameObject;
		Debug.Log ("ready to change scene" + collObj.tag);

		if (collObj.tag == Globals.playerTag) {
			Debug.Log ("changed position");
			Application.LoadLevel (targetScene);
			collObj.transform.position = newPos;
		}

	}
}
