using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class fetchPlayerHealth : MonoBehaviour {
	private Image image;
	private Sprite[] healths;
	private StatusManager status;
	// Use this for initialization
	void Start () {	
		image = GetComponent<Image> ();
		status = GameObject.FindGameObjectWithTag("Player").GetComponent<StatusManager>();
		healths = new Sprite[17];
		for (int i = 0; i <= 16; i++) {
			string assetname_prefix = "gui/player-health/health-";
			string number = i.ToString("D2");
			healths[i] = Resources.Load(assetname_prefix + number, typeof(Sprite)) as Sprite;
			if (!healths[i])
				Debug.LogError("RESOURCE: player health information init failed");
		}
	}
	
	// Update is called once per frame
	void Update () {
		int playerHealth = Mathf.Clamp (status.playerHealth, 0, 16);
		image.sprite = healths [playerHealth];
	}
}
