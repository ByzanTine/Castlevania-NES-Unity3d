using UnityEngine;
using System.Collections;

public class MonsterSpawner : MonoBehaviour {

	// all public var need to be init in Inspector
	public GameObject  demonPrefab;
	public int numberOfMonsters;
	public Vector2 createAt;
	private int demonNum = 0;
	public float creationDelayInSec = 0.5f;
	public float refreshTime = 5.0f;
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
				if(collider2D.enabled == false)
					return true;
			}
			yield return new WaitForSeconds (refreshTime/2f);
			if(collider2D.enabled == false)
				return true;
			yield return new WaitForSeconds (refreshTime/2f);

			demonNum = 0;
		}
	}

	void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player") {
			Debug.Log ("Disabled collider");
			StartCoroutine(waitToReset());


		}
	}

	IEnumerator waitToReset()
	{
		collider2D.enabled = false;
		yield return new WaitForSeconds (refreshTime);
		collider2D.enabled = true;

	}
}
