using UnityEngine;
using System.Collections;

public class ItemMotion : MonoBehaviour {

	public float speedY = -1.0f;
	public float perishInSec = 10.0f;

	public float Amplitude = 0.5f;
	public float omega = 2f;
	//must be specified in inspector
	public Globals.ItemName itemName;

	private float SpeedX;
	private float time;
	private StatusManager status;
	private CollisionManager cmScript;

	StatusManager smScript;
	WhipAttackManager attManager;
	SubWeaponManager wepManager;

	// Use this for initialization
	void Start () {
		time = Time.time;
		cmScript = GetComponent<CollisionManager> ();
		if (!cmScript)
			Debug.LogError("CollisionManager can't retrieve, check your prefab if it's linked");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		move ();
	}

	void move()
	{
		if (speedY < 0) {


			if (cmScript.isWallOn (Globals.Direction.Bottom)) {
				hitGround();
			}
			SpeedX = Amplitude * Mathf.Cos(omega * (Time.time - time));
			Vector3 pos = this.transform.position;
			pos.y += speedY * Time.fixedDeltaTime;
			pos.x += SpeedX * Time.fixedDeltaTime;
			this.transform.position = pos;

		}
	}

	public void hitGround()
	{
		speedY = 0;
		Destroy (this.gameObject, perishInSec);
	}

	void OnTriggerEnter2D( Collider2D coll ) {
		GameObject collidedObj = coll.gameObject;
		if (collidedObj.tag == Globals.playerTag) 
		{
			itemPickedUp(collidedObj);		              
		}
	}



	void itemPickedUp(GameObject plObj)
	{
		smScript = plObj.GetComponent<StatusManager> ();
		attManager = plObj.GetComponent<WhipAttackManager> ();
		wepManager = plObj.GetComponent<SubWeaponManager> ();

		switch (itemName)
		{
		case Globals.ItemName.Money_S:			
		case Globals.ItemName.Money_M:					
		case Globals.ItemName.Money_L:		
			GameObject moneySE = Resources.Load (Globals.SEdir + "moneySE") as GameObject;
			Instantiate (moneySE, transform.position, Quaternion.identity);
			break;
			
		case Globals.ItemName.SmallHeart:		
		case Globals.ItemName.LargeHeart:	
			GameObject heartSE = Resources.Load (Globals.SEdir + "heartSE") as GameObject;
			Instantiate (heartSE, transform.position, Quaternion.identity);
			break;
			
		case Globals.ItemName.WhipUp:
			
		case Globals.ItemName.Dagger:
			
		case Globals.ItemName.Axe:
			
		case Globals.ItemName.HolyWater:
			
		case Globals.ItemName.StopWatch:
			GameObject upgradeSE = Resources.Load (Globals.SEdir + "upgradeSE") as GameObject;
			Instantiate (upgradeSE, transform.position, Quaternion.identity);
			break;

			
		case Globals.ItemName.Rosary:
			GameObject rosarySE = Resources.Load (Globals.SEdir + "rosarySE") as GameObject;
			Instantiate (rosarySE, transform.position, Quaternion.identity);
			break;
		case Globals.ItemName.BossHeart:
			
			GameObject deathSE = Resources.Load (Globals.SEdir + "WinMusic") as GameObject;
			Instantiate (deathSE, transform.position, Quaternion.identity);
			
			break;
			
		default:
			GameObject defaultSE = Resources.Load (Globals.SEdir + "heartSE") as GameObject;
			Instantiate (defaultSE, transform.position, Quaternion.identity);
			break;
		}

		switch (itemName)
		{
		case Globals.ItemName.Money_S:
			smScript.score += 100;
			Debug.Log ("fetched small money");
			break;

		case Globals.ItemName.Money_M:		
			smScript.score += 400;
			Debug.Log ("fetched medium money");
			break;

		case Globals.ItemName.Money_L:		
			smScript.score += 700;
			Debug.Log ("fetched large money");
			break;

		case Globals.ItemName.SmallHeart:		
			smScript.heartNum += 1;
			Debug.Log ("fetched heart");
			break;

		case Globals.ItemName.LargeHeart:	
			smScript.heartNum += 5;
			Debug.Log ("fetched heart");
			break;
		
		case Globals.ItemName.WhipUp:
			if(attManager.whipLevel < 3)
				attManager.UpgradWhip();
			Debug.Log ("fetched Morning Star, whip powered up");
			break;

		case Globals.ItemName.Rosary:

			Debug.Log ("fetched Rosery, clear stage");


			GameObject[] allEnemy = GameObject.FindGameObjectsWithTag(Globals.EnemyTag);
			foreach(GameObject enemy in allEnemy)
			{
				OnWhipEvent whipScript = enemy.GetComponent<OnWhipEvent>();
				
				if(whipScript != null)
				{
					whipScript.onWhipEnter();
				}
			}
			break;

		case Globals.ItemName.Dagger:
			wepManager.weaponPickedUp(Globals.SubWeapon.Dagger);
			break;

		case Globals.ItemName.Axe:
			wepManager.weaponPickedUp(Globals.SubWeapon.Axe);
			break;		
		
		case Globals.ItemName.HolyWater:
			wepManager.weaponPickedUp(Globals.SubWeapon.HolyWater);
			break;
		
		case Globals.ItemName.StopWatch:
			wepManager.weaponPickedUp(Globals.SubWeapon.StopWatch);
			break;
		
		case Globals.ItemName.BossHeart:
			getBossHeart();
			break;

		case Globals.ItemName.ChickenLeg:
			smScript.playerHealth += 6;
			Mathf.Clamp(smScript.playerHealth, 0, Globals.maxBossHealth);
			break;

		default:
			break;
		}

		Debug.Log ("Player picked Up");
		if(audio)
			audio.Play ();
		Destroy (this.gameObject);
	}

	void getBossHeart()
	{
		while(smScript.playerHealth <= Globals.maxPlayerHealth)
		{
			smScript.playerHealth++;
		}
		smScript.score += 10000;
		smScript.bossDefeated = true;
		GameObject deathSE = Resources.Load (Globals.SEdir + "WinMusic") as GameObject;
		Instantiate (deathSE, transform.position, Quaternion.identity);

		if(Application.loadedLevelName.Equals("Custom_00"))
		{
			smScript.playerDie();
		}
	}


}
