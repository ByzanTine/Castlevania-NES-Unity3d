using UnityEngine;
using System.Collections;

public class transEvent : MonoBehaviour {

	// Use this for initialization
	void Start () {
		buryPlayer ();
	}

	void buryPlayer()
	{
		Destroy (GameObject.FindGameObjectWithTag(Globals.playerTag));

		if(Application.loadedLevelName.Equals("Custom_00"))
		{
			vampirelize();
		}
		else
		{
			Application.LoadLevel (Application.loadedLevel);
		}
	}

	void vampirelize()
	{

	}
}
