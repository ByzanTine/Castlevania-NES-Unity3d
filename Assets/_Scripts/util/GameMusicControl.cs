using UnityEngine;
using System.Collections;

public class GameMusicControl : MonoBehaviour {

	private static GameMusicControl instance = null;
	private Vector3 velocity = Vector3.zero;

//	public static GameMusicControl Instance 
//	{
//		get { return instance; }
//	}
	void Awake() {
		if (instance != null && instance != this)// && instCount < lifeTime) 
		{
			Destroy(instance.gameObject);
		}
		instance = this;
		DontDestroyOnLoad (this.gameObject);
	}

}
