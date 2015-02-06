using UnityEngine;
using System.Collections;

public class customLevelEntrance : MonoBehaviour {
	public Vector3 newPos;
	private bool entering = false;

	void OnTriggerStay2D(Collider2D other) {

		if (other.GetComponent<PlayerControllerVampire>() == null) {
			return;
		}
		Debug.Log("ENTRANCE: Enter Trigger of Going into castlevania");
		if (!entering)
			StartCoroutine(GoToPortal(other));
		
		
	}
	
	IEnumerator GoToPortal(Collider2D other) {
		
		yield return new WaitForSeconds (2.0f);
		entering = true;
		float prepXcenter = transform.position.x;
		GameObject pc = other.gameObject;
		Debug.Log("ENTRANCE: go to portal");

		// using current speed system to move the object 
		Vector2 destination = new Vector2(prepXcenter, pc.transform.position.y);

		
		yield return StartCoroutine(other.GetComponent<StairManager>().MoveObject(pc.transform.position, destination, 2.5f));
		
		
		// Then do a pixel correction 
		transform.position = new Vector2(prepXcenter, transform.position.y);
		// Then correct the object facing

		
	
		
		Destroy (GameObject.FindGameObjectWithTag(Globals.playerTag));
		Destroy (GameObject.Find ("InputManager"));
		
		Application.LoadLevel ("custom_01");
		pc.transform.position = newPos;
		

		
	}
}
