using UnityEngine;
using System.Collections;

public class transEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		buryPlayer ();
	}

	void buryPlayer()
	{
		Debug.Log("destroying player");
		Destroy (GameObject.FindGameObjectWithTag(Globals.playerTag));
		Debug.Log("destroyed player");

		if(Application.loadedLevelName.Equals("Custom_00"))
		{
			Debug.Log("vampirelizing");

			vampirelize();
		}
		else
		{

			Debug.Log("reloading");

			Application.LoadLevel (Application.loadedLevel);
		}
	}

	void vampirelize()
	{

	}
}
