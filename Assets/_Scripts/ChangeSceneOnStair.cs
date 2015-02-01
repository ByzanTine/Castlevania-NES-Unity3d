using UnityEngine;
using System.Collections;

public class ChangeSceneOnStair : MonoBehaviour {

	// both var must be initialized in inspector
	public string targetScene;
	public int thisPortalNum;

	void OnTriggerStay2D( Collider2D coll ) {
		GameObject collObj = coll.gameObject;
		Debug.Log ("ready to change scene on stair" + collObj.tag);

		if (collObj.tag == Globals.playerTag) {
			PlayerController plScript = collObj.GetComponent<PlayerController>();
			StairManager smScript = collObj.GetComponent<StairManager>();
			StatusManager stScript = collObj.GetComponent<StatusManager>();
			if(smScript.isOnStair())
			{
				if((targetScene.Equals("Scene_01") && plScript.facingRight == false)
					||(targetScene.Equals("Scene_02") && plScript.facingRight == true))
				{
					Debug.Log ("changed to " + targetScene +" on stair");
					stScript.portalNum = thisPortalNum;

					stScript.enterStairPortal(targetScene);
				}
			}

		}

	}
}
