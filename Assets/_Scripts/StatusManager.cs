using UnityEngine;
using System.Collections;

public class StatusManager : MonoBehaviour {

	public static int score = 0;
	public static int heartNum = 0;
	public static int playerHealth = Globals.maxPlayerHealth;
	public static int bossHealth = Globals.maxBossHealth;

	private static StatusManager playerInstance = null;
	private static float prevPos = 0.0f;
	public bool savePos = false;

//	public static StatusManager PlayerInstance 
//	{
//		get { return playerInstance; }
//	}

	// Use this for initialization
	void Awake () {
		instanceControl ();
		positionControl ();
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
