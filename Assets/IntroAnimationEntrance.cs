using UnityEngine;
using System.Collections;

public class IntroAnimationEntrance : MonoBehaviour {
	public Vector3 newPos;
	private bool entering = false;

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

			
			yield return StartCoroutine(other.GetComponent<StairManager>().MoveObject(pc.transform.position, destination, 5.0f));
			
			
			// Then do a pixel correction 
			transform.position = new Vector2(prepXcenter, transform.position.y);
			// Then correct the object facing
			// small hack to reset the Speed when trying to adjust the facing
			// Don't let the player controller do a fixed update that mess with the facing
			pc.GetComponent<Animator> ().SetInteger ("Speed", 0);

			pc.GetComponent<Animator> ().SetBool("FaceWall", true);
			yield return new WaitForSeconds(3.0f);
			pc.GetComponent<Animator> ().SetBool("FaceWall", false);

			Destroy (GameObject.Find ("Player"));
			Destroy (GameObject.Find ("InputManager"));

			Application.LoadLevel ("Scene_00");
			pc.transform.position = newPos;
			
		}
		
	}
}
