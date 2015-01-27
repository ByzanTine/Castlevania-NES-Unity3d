using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class fetchHeart : MonoBehaviour {
	private Text text;
	private StatusManager status;
	// Use this for initialization
	void Start () {
		status = GameObject.FindGameObjectWithTag("Player").GetComponent<StatusManager>();
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		// replace this coming int
		int heartNum = Mathf.Clamp (status.heartNum, 0, 99);
		string score = heartNum.ToString ("D2");
		text.text = score;
	}
}
