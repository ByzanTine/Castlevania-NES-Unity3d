using UnityEngine;
using System.Collections;

public class CreateMoster : MonoBehaviour {

	// all public var need to be init in Inspector
	public GameObject  demonPrefab;
	public int numberOfMonsters;
	public Vector2 createAt;
	private int demonNum = 0;
	public float creationDelayInSec = 0.5f;
	private float refreshTime = 5.0f;
	// Use this for initialization
	void Start () {
	}
	
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			StartCoroutine(createMoster());
		}
	}

	IEnumerator createMoster()
	{
		while (collider2D.enabled == true) {
			for (; demonNum < numberOfMonsters; ++demonNum) {
				GameObject gameObj = Instantiate (demonPrefab) as GameObject;
				gameObj.transform.position = 
					new Vector3 (transform.position.x + createAt.x, 
            	transform.position.y + createAt.y, 0);
				yield return new WaitForSeconds (creationDelayInSec);
			}
			yield return new WaitForSeconds (refreshTime);
			demonNum = 0;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			Debug.Log ("Disabled collider");
			collider2D.enabled = false;
		}
	}
}
