using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	private Transform target;

//	private static CameraMove instance;
//	void Awake()
//	{
//		if (instance != null && instance != this) 
//		{
//			Destroy (this.gameObject);
//			return;
//		}
//		else 
//		{
//			instance = this;
//		}
//		DontDestroyOnLoad(this.gameObject);
//	}

	// Update is called once per frame
	void Update () {

		target = GameObject.FindGameObjectWithTag ("Player").transform;

		if (target)
		{
			Vector3 point = camera.WorldToViewportPoint(target.position);
			Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
			Vector3 destination = transform.position + delta;
			destination.y = transform.position.y;
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
		}
	}
}
