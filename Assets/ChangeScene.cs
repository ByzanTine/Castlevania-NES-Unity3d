using UnityEngine;
using System.Collections;

public class ChangeScene : MonoBehaviour {

	public string targetScene;
	public Vector3 newPos = new Vector3(0,0,0);

	void OnTriggerEnter2D( Collider2D coll ) {
		GameObject collObj = coll.gameObject;
		Debug.Log ("rdy cg sce" + collObj.tag);

		if (collObj.tag == "Player") {
			Debug.Log ("cg sce");
			Application.LoadLevel (targetScene);
			collObj.transform.position = newPos;
		}

	}
}
