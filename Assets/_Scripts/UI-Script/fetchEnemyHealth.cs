using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class fetchEnemyHealth : MonoBehaviour {
	private Image image;
	private Sprite[] healths;
	private BossMotion bossStatus;
	private int bossHealth;
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

		GameObject boss = GameObject.FindGameObjectWithTag ("Boss");
		if(boss)
		{
			Debug.Log("Boss found");
			bossStatus = boss.GetComponent<BossMotion>();
			if(!bossStatus)
				Debug.LogError("Boss scripts not found");
		}
		bossHealth = Globals.maxBossHealth;
		image.sprite = healths [bossHealth];
	}
	
	// Update is called once per frame
	void Update () {
		if (bossStatus != null) {
			bossHealth = bossStatus.bossHealth;
			image.sprite = healths [bossHealth];
		}

			
	}
}
