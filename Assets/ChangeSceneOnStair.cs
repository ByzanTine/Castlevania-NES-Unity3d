using UnityEngine;
using System.Collections;

public class ChangeSceneOnStair : MonoBehaviour {
	
	public string targetScene;
	public Vector3 newPos = new Vector3(0,0,0);
	
	void OnTriggerEnter2D( Collider2D coll ) {
		GameObject collObj = coll.gameObject;
		Debug.Log ("rdy to chg sce on stair" + collObj.tag);

		if (collObj.tag == "Player") {
			PlayerController plScript = collObj.GetComponent<PlayerController>();
			StairManager smScript = collObj.GetComponent<StairManager>();
			if(smScript.isOnStair())
			{
				if((targetScene.Equals("Scene_01") && plScript.facingRight == false)
					||(targetScene.Equals("Scene_02") && plScript.facingRight == true))
				{
					Debug.Log ("chged to " + targetScene +" on stair");
					Application.LoadLevel (targetScene);
					collObj.transform.position = newPos;
				}
			}

		}

	}
}
