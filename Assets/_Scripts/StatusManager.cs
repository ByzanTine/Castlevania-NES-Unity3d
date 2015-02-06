using UnityEngine;
using System.Collections;

public class StatusManager : MonoBehaviour {

	public int score = 0;
	public int curTime = 300;
	public int heartNum = 0;
	public static int lives = 3;
	public int playerHealth = Globals.maxPlayerHealth;

	private static StatusManager playerInstance = null;
	private static float prevPos = 0.0f;

	public int portalNum = 0;
	public bool bossDefeated = false;
//	public Vector3 transformedVec = new Vector3 (float.MaxValue,float.MaxValue,float.MaxValue);
//	private bool isTransed = true;
	private string targetScene;
	private bool isDying = false;
	private Animator animator;


	void OnLevelWasLoaded(int level)
	{
		if(Application.loadedLevelName.Equals("Scene_00") ||
		   Application.loadedLevelName.Equals("Custom_00"))
		{
			lives = 3;
		}
	}

	public void enterStairPortal(string levelName) {

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

	void Start()
	{
		animator = GetComponent<Animator>();

		InputManager.Instance.OnKeyDown_G += HandleOnKeyDown_G;

	}

	void Update()
	{
		if (playerHealth <= 0 && !isDying) 
		{
			isDying = true;
			playerDie();	
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



		GameObject BGM = Resources.Load (Globals.SEdir + "BGMusic") as GameObject;
		Instantiate (BGM, transform.position, Quaternion.identity);
	}

	public void changeBGM()
	{
		GameObject BGM = Resources.Load (Globals.SEdir + "BGMcus") as GameObject;	
		Instantiate (BGM, transform.position, Quaternion.identity);
	}

	void HandleOnKeyDown_G () {
		// Gibson mode activated 
		heartNum += 10;
		playerHealth = Globals.maxPlayerHealth;
		curTime += 30;
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
//		yield return new WaitForSeconds (0.5f);
		InputManager.Instance.disableControl = true;
		lives--;
		animator.SetBool ("Dead", true);
		GameObject SimonDead = Resources.Load (Globals.SEdir + "SimonDead") as GameObject;
		Instantiate (SimonDead, transform.position, Quaternion.identity);

		yield return new WaitForSeconds (2.5f);

		Destroy (GameObject.Find("InputManager"));

		GameObject transEvent = Resources.Load ("Prefab/transEvent") as GameObject;
		Instantiate (transEvent, transform.position, Quaternion.identity);
	}


	void OnDestroy () {
		Debug.Log ("saved position x:" + transform.position.x);
		PlayerPrefs.SetFloat("PreviousPlayerPosition", transform.position.x);
	}
}
