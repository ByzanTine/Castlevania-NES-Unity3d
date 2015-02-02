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
		StatusManager smScript = plObj.GetComponent<StatusManager> ();
		WhipAttackManager attManager = plObj.GetComponent<WhipAttackManager> ();
		SubWeaponManager wepManager = plObj.GetComponent<SubWeaponManager> ();

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
				attManager.whipLevel += 1;
			Debug.Log ("fetched Morning Star, whip powered up");
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

		default:
			break;
		}

		Debug.Log ("Player picked Up");
		Destroy (this.gameObject);
	}
}
