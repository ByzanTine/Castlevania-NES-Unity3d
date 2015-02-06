using UnityEngine;
using System.Collections;

public class transEvent : MonoBehaviour {
	public GameObject vampirePlayer;
	// Use this for initialization
	void Start () {
		buryPlayer ();
	}

	void buryPlayer()
	{
		GameObject playerObj = GameObject.FindGameObjectWithTag (Globals.playerTag);

		StatusManager smScript = playerObj.GetComponent<StatusManager>();
		if(StatusManager.lives <= 0)
		{
			StartCoroutine(gameOver());
		}
		else if(Application.loadedLevelName.Equals("Custom_00")
		        && smScript.bossDefeated)
		{
			StartCoroutine(vampirelize());
		}
		else
		{
			StartCoroutine(loadingScene());
		}

		Destroy (playerObj);

	}

	IEnumerator gameOver()
	{
		while (GameObject.FindGameObjectWithTag(Globals.playerTag)) {
			yield return new WaitForSeconds(0.2f);
		}
		
		Debug.Log ("reloading title scene");
		Application.LoadLevel (0);
	}

	IEnumerator loadingScene()
	{
		while (GameObject.FindGameObjectWithTag(Globals.playerTag)) {
			yield return new WaitForSeconds(0.2f);
		}
		
		Debug.Log ("reloading scene");
		Application.LoadLevel (Application.loadedLevel);
	}

	IEnumerator vampirelize()
	{
		while (GameObject.FindGameObjectWithTag(Globals.playerTag)) {
			yield return new WaitForSeconds(1.0f);
		}
						
		Debug.Log ("ANIMATION: GOING TO DOOR");
		GameObject gb = Instantiate (vampirePlayer, transform.position, Quaternion.identity) as GameObject;

	}
}
