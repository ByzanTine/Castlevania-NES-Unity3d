using UnityEngine;
using System.Collections;

public class ChangeSceneOnStair : MonoBehaviour {

	// both var must be initialized in inspector
	public string targetScene;
	public int thisPortalNum;

//	private static ChangeSceneOnStair portalInstance = null;

	void Awake () {
//		instanceControl ();
	}

//	void instanceControl()
//	{
//		if (portalInstance != null && portalInstance != this) 
//		{
//			Destroy (this.gameObject);
//			return;
//		}
//		else 
//		{
//			portalInstance = this;
//		}
//		DontDestroyOnLoad(this.gameObject);
//	}
	
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
					Application.LoadLevel (targetScene);
					stScript.portalNum = thisPortalNum;
				}
			}

		}

	}
}
