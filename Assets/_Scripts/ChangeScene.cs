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

			if(targetScene.Equals("Scene_Title_Screen"))
			{
				StatusManager smScript = collObj.GetComponent<StatusManager> ();

				if(smScript.bossDefeated)
				{
					Destroy (GameObject.Find("InputManager"));
					Destroy(collObj);

					CameraMove cmScript = Camera.main.camera.GetComponent<CameraMove>();
					cmScript.defrost();

					Application.LoadLevel (targetScene);

				}
			}
			else
			{
				Debug.Log ("changed position");
				Application.LoadLevel (targetScene);
				collObj.transform.position = newPos;
			}
		}
	}
}
