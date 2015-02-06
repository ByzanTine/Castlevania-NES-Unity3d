using UnityEngine;
using System.Collections;

public class SceneEntrance : MonoBehaviour {
	public Vector3 newPos;
	private bool entering = false;
	void Start() {
		transform.GetChild (0).GetComponent<SpriteRenderer> ().enabled = false;
	}
	void OnTriggerStay2D(Collider2D other) {
		Debug.Log("ENTRANCE: Enter Trigger of Going into castlevania");

		if (!entering)
			StartCoroutine(GoToPortal(other));

		
	}

	IEnumerator GoToPortal(Collider2D other) {

		if (!other.gameObject.GetComponent<PlayerController>().grounded)
			yield return null;
		else {
			entering = true;
			float prepXcenter = transform.position.x;
			GameObject pc = other.gameObject;
			Debug.Log("ENTRANCE: go to portal");
			GameObject.FindGameObjectWithTag ("Input").GetComponent<InputManager> ().disableControl = true;
			// using current speed system to move the object 
			Vector2 destination = new Vector2(prepXcenter, pc.transform.position.y);
			if (pc.transform.position.x < prepXcenter)	
				pc.GetComponent<Animator> ().SetInteger ("Speed", 1);
			else 
				pc.GetComponent<Animator> ().SetInteger ("Speed", -1);
			// determine the animationRatio by the distance from the center
			float animRatio = Mathf.Abs (pc.transform.position.x - prepXcenter) / 
				(Globals.StairUpTriggerColliderWidth / 2 + Globals.playerWidth / 2);
			// Debug.Log ("animRatio: " + animRatio);
			animRatio = Mathf.Clamp (animRatio, 0.01f, 1.0f);

			GameObject portalSE = Resources.Load (Globals.SEdir + "portalSE") as GameObject;
			Instantiate (portalSE, transform.position, Quaternion.identity);
			
			yield return StartCoroutine(other.GetComponent<StairManager>().MoveObject(pc.transform.position, destination, animRatio * 1.0f));

			
			// Then do a pixel correction 
			transform.position = new Vector2(prepXcenter, transform.position.y);
			// Then correct the object facing
			// small hack to reset the Speed when trying to adjust the facing
			// Don't let the player controller do a fixed update that mess with the facing
			pc.GetComponent<Animator> ().SetInteger ("Speed", 0);

			transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;

			Vector2 portalPos = new Vector2(prepXcenter + 0.2f, pc.transform.position.y);
			pc.GetComponent<Animator> ().SetInteger ("Speed", 1);

			yield return StartCoroutine(other.GetComponent<StairManager>().MoveObject(pc.transform.position, portalPos, animRatio * 1.0f));

			GameObject.FindGameObjectWithTag ("Input").GetComponent<InputManager> ().disableControl = false;
			Application.LoadLevel ("Scene_01");
			pc.transform.position = newPos;

		}

	}
}
