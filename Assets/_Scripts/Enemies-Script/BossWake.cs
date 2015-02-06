using UnityEngine;
using System.Collections;

public class BossWake : MonoBehaviour {

	public GameObject borderPrefab;

	// all public var need to be init in Inspector
	// Use this for initialization
	void Start () {
	}
	
	void OnTriggerEnter2D(Collider2D other){
		Debug.Log("something comming to boss");

		if (other.gameObject.tag == "Player") 
		{
			Debug.Log("player comming to boss");
			StartCoroutine(bossWake());
			collider2D.enabled = false;
		}
	}

	IEnumerator bossWake()
	{

		float verExtent = Camera.main.camera.orthographicSize;
		float horExtent = verExtent * Screen.width / Screen.height;
		BossMotion bmScript = GetComponentInParent<BossMotion>();
		Transform bossTransform = GetComponentInParent<Transform> ();

		CameraMove cmScript = Camera.main.camera.GetComponent<CameraMove>();
		cmScript.freeze (bossTransform.position.x);


		GameObject leftBorder = Instantiate (borderPrefab) as GameObject;
		leftBorder.transform.position = new Vector2(bossTransform.position.x - horExtent, 0.5f);

		GameObject bossMusic = Resources.Load (Globals.SEdir + "BossMusic") as GameObject;
		if(Application.loadedLevelName == "Custom_01")
			bossMusic = Resources.Load (Globals.SEdir + "BossCus") as GameObject;
		Instantiate (bossMusic, transform.position, Quaternion.identity);

//		GameObject rightBorder = Instantiate (borderPrefab) as GameObject;
//		rightBorder.transform.position = new Vector2(bossTransform.position.x + horExtent, 0.5f);

		yield return new WaitForSeconds (3.0f);

		bmScript.wakeUp ();
	}

}
