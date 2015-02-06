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
		Destroy (GameObject.FindGameObjectWithTag(Globals.playerTag));

		if(Application.loadedLevelName.Equals("Custom_00"))
		{
			StartCoroutine(vampirelize());
		}
		else
		{
			Application.LoadLevel (Application.loadedLevel);
		}
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
