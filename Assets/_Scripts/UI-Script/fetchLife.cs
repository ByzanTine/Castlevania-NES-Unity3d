using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class fetchLife : MonoBehaviour {
	private Text text;
//	private StatusManager status;
	// Use this for initialization
	void Start () {
//		status = GameObject.FindGameObjectWithTag("Player").GetComponent<StatusManager>();
		text = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		// replace this coming int
		int lifeNum = StatusManager.lives;
		int life = Mathf.Clamp (lifeNum, 0, 99);
		string score = life.ToString ("D2");
		text.text = score;
	}
}
