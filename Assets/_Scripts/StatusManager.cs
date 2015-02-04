using UnityEngine;
using System.Collections;

public class StatusManager : MonoBehaviour {

	public int score = 0;
	public int heartNum = 0;
	public int playerHealth = Globals.maxPlayerHealth;

	private static StatusManager playerInstance = null;
	private static float prevPos = 0.0f;

	public int portalNum = 0;
	public bool bossDefeated = false;
//	public Vector3 transformedVec = new Vector3 (float.MaxValue,float.MaxValue,float.MaxValue);
//	private bool isTransed = true;
	private string targetScene;
	private bool isDying = false;


	public void enterStairPortal(string levelName) {
//		if (levelName == "Scene_00")
//			print("Woohoo");
//		else if (levelName == "Scene_01")
//		{
//			if(portalNum == 1)
//				this.gameObject.transform.position = Globals.subPortalTarget1;
//			else if(portalNum == 2)
//				this.gameObject.transform.position = Globals.subPortalTarget2;
//		}
//		else if (levelName == "Scene_02")
//		{
//			if(portalNum == 1)
//				this.gameObject.transform.position = Globals.groundPortalTarget1;
//			else if(portalNum == 2)
//				this.gameObject.transform.position = Globals.groundPortalTarget2;
//		}

		if (portalNum != 0) {
			portalNum = 0;
//			transformedVec = transform.position;
//			isTransed = false;
			targetScene = levelName;
			Debug.Log ("portal transform done:" + transform.position);
			Application.LoadLevel (targetScene);
			Debug.Log ("Transed");

//			StartCoroutine(delayingTrans());

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
		if (playerHealth <= 0 && !isDying) 
		{
			isDying = true;
			playerDie();	
		}
	}

//	IEnumerator delayingTrans()
//	{
//		//transform.position = transformedVec;
//		yield return new WaitForSeconds(0.2f);
//
//	}


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

		GameObject BGM = Resources.Load (Globals.SEdir + "BGMusic") as GameObject;
		Instantiate (BGM, transform.position, Quaternion.identity);
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


	public void playerDie()
	{
		StartCoroutine (playerDying());

	}

	IEnumerator playerDying()
	{

		yield return new WaitForSeconds (0.5f);

		GameObject bossMusic = Resources.Load ("Prefab/AudioObject/SimonDead") as GameObject;
		Instantiate (bossMusic, transform.position, Quaternion.identity);

		yield return new WaitForSeconds (1.5f);

		Destroy (GameObject.Find("InputManager"));
		Destroy (this.gameObject);

		Application.LoadLevel (Application.loadedLevel);
	}


	void OnDestroy () {
		Debug.Log ("saved position x:" + transform.position.x);
		PlayerPrefs.SetFloat("PreviousPlayerPosition", transform.position.x);
	}
}
