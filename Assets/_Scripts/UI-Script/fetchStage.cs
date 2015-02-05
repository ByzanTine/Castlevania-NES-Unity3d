using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class fetchStage : MonoBehaviour {
	private Text text;
	private int LevelId;
	// Use this for initialization
	void Start () {
		text = GetComponent<Text> ();
		LevelId = Application.loadedLevel - 2;
	}

	void OnLevelWasLoaded(int level) {
//		Debug.Log = 
		if(level >= 2)
			LevelId = level;
		
	}

	
	// Update is called once per frame
	void Update () {
		// replace this coming int

		string score = LevelId.ToString ("D2");
		text.text = score;
	}
}
