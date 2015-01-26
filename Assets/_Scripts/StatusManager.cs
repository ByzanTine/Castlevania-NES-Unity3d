using UnityEngine;
using System.Collections;

public class StatusManager : MonoBehaviour {

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
			PlayerCollisionManager pcmScript = 
				playerInstance.gameObject.GetComponent<PlayerCollisionManager>();
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
