using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OnStartBlink : MonoBehaviour {

	private Text text;

	public string levelName = "Intro-Enter";
	void Start() {
		text = GetComponent<Text> ();
	}
	// Update is called once per frame
	void Update () {
		OnEnterBlink ();
	}

	void OnEnterBlink () {
		if (Input.GetKeyDown(KeyCode.Return)) {
			Debug.Log("Enter Pressed");
			StartCoroutine(Blink());
		}
	}

	IEnumerator Blink () {
		for (int i = 0; i < 5; i++) {
			Debug.Log("turn once");
			text.enabled = false;
			yield return new WaitForSeconds (0.1f);
			text.enabled = true;
			yield return new WaitForSeconds (0.1f);
		}
		// enter level
		Application.LoadLevel (levelName);
	}
}
