using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class fetchScore : MonoBehaviour {
	private Text text;
	private StatusManager status;
	// Use this for initialization
	void Start () {
		status = GameObject.FindGameObjectWithTag("Player").GetComponent<StatusManager>();
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		string score = status.score.ToString ("D6");
		text.text = score;
	}
	
}
