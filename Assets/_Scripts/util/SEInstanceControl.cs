using UnityEngine;
using System.Collections;

public class SEInstanceControl : MonoBehaviour {

	private static SEInstanceControl instance = null;
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
		DontDestroyOnLoad(this.gameObject);
		
	}
}
