using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class updateTimer : MonoBehaviour {
	private Text text;
//	int initTime = 300;
//	public static int curTime;
	float timerTime;
	private StatusManager status;

	// Use this for initialization
	void Start () {
//		curTime = initTime;
//		timerTime = Time.time;
		status = GameObject.FindGameObjectWithTag(Globals.playerTag).GetComponent<StatusManager>();

		text = GetComponent<Text> ();
	}

//	// Use this for initialization
//	void Start () {
//		text = GetComponent<Text> ();
//	}
	
	// Update is called once per frame
	void Update () {
		// wait for a second
		if (Time.time - timerTime > 1.0f && status.curTime > 0) {
			timerTime = Time.time;
			status.curTime--;
			if (status.curTime < 30) {
				// start beep
				GameObject timerSE = Resources.Load (Globals.SEdir + "timerSE") as GameObject;
				Instantiate (timerSE, transform.position, Quaternion.identity);

				if (status.curTime == 0) {
					// Die
					status.playerDie();
				}
			}

			// update the text
			text.text = status.curTime.ToString ("D4");

		}


	}
}
