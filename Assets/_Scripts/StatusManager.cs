using UnityEngine;
using System.Collections;

public class StatusManager : MonoBehaviour {

	public int score = 0;
	public int heartNum = 0;
	public int playerHealth = Globals.maxPlayerHealth;
	public int bossHealth = Globals.maxBossHealth;

	private static StatusManager playerInstance = null;
	private static float prevPos = 0.0f;

	public int portalNum = 0;
	public Vector3 transformedVec = new Vector3 (float.MaxValue,float.MaxValue,float.MaxValue);
	private bool isTransed = true;

	void OnLevelWasLoaded(int level) {
		if (level == 0)
			print("Woohoo");
		else if (level == 1)
		{
			if(portalNum == 1)
				this.gameObject.transform.position = Globals.subPortalTarget1;
			else if(portalNum == 2)
				this.gameObject.transform.position = Globals.subPortalTarget2;
		}
		else if (level == 2)
		{
			if(portalNum == 1)
				this.gameObject.transform.position = Globals.groundPortalTarget1;
			else if(portalNum == 2)
				this.gameObject.transform.position = Globals.groundPortalTarget2;
		}

		if (portalNum != 0) {
			portalNum = 0;
			transformedVec = transform.position;
			isTransed = false;
			Debug.Log ("portal transform done:" + transform.position);
		}

}


//	public static StatusManager PlayerInstance 
//	{
//		get { return playerInstance; }
//	}

	// Use this for initialization
	void Awake () {
		instanceControl ();
		positionControl ();
	}

	void Update()
	{
		if (!isTransed)
		{
			transform.position = transformedVec;
			Debug.Log ("Transed");
			isTransed = true;
		}
	}

	void instanceControl()
	{
		if (playerInstance != null && playerInstance != this) 
		{
			CollisionManager pcmScript = 
				playerInstance.gameObject.GetComponent<CollisionManager>();
			pcmScript.reset();
			Destroy (this.gameObject);
			return;
		}
		else 
		{
			playerInstance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	public bool savePos = false;

	void positionControl()
	{
		// If the PreviousPlayerPosition already exists, read it
		if (PlayerPrefs.HasKey("PreviousPlayerPosition")) {                   // 2
			prevPos = PlayerPrefs.GetFloat("PreviousPlayerPosition");
		}
		// Assign the prevPos to PreviousPlayerPosition
		PlayerPrefs.SetFloat("PreviousPlayerPosition", prevPos); 

		if(savePos)
			transform.position = new Vector3 (prevPos, transform.position.y, transform.position.z);
	}
	
	void OnDestroy () {
		Debug.Log ("saved position x:" + transform.position.x);
		PlayerPrefs.SetFloat("PreviousPlayerPosition", transform.position.x);
	}
}
