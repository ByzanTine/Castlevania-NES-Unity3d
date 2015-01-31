using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class fetchEnemyHealth : MonoBehaviour {
	private Image image;
	private Sprite[] healths;
	// Use this for initialization
	void Start () {	
		image = GetComponent<Image> ();
		healths = new Sprite[17];
		for (int i = 0; i <= 16; i++) {
			string assetname_prefix = "gui/enemy-health/health-";
			string number = i.ToString("D2");
			healths[i] = Resources.Load(assetname_prefix + number, typeof(Sprite)) as Sprite;
			if (!healths[i])
				Debug.LogError("RESOURCE: player health information init failed");
		}
	}
	
	// Update is called once per frame
	void Update () {
		int coming_int = 16;
		image.sprite = healths [coming_int];
	}
}
