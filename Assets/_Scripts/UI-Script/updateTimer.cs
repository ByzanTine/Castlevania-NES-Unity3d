using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class updateTimer : MonoBehaviour {
	private Text text;
	int initTime = 300;
	int curTime;
	float timerTime;
	// Use this for initialization
	void Start () {
		curTime = initTime;
		timerTime = Time.time;
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		// wait for a second
		if (Time.time - timerTime > 1.0f) {
			timerTime = Time.time;
			curTime--;
			// update the text
			text.text = curTime.ToString ("D3");

		}


	}
}
