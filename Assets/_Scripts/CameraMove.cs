using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour {
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	private Transform target;

	private GameObject map; // retrieve map for calculating the size of the background
	private float minX; // bound to left 
	private float maxX;	// bound to right
	private bool isFrozen;
	private float frozenPos;

	private GameObject playerObj;
	// assume the bg is x is at the center 
	void Start() {
		isFrozen = false;
		frozenPos = 0.0f;
		map = GameObject.FindGameObjectWithTag (Globals.MapTag);
		if (!map) {
			Debug.LogError("There is no map background in the scene, make sure you have a object with a tag Map");
		}
		float verExtent = Camera.main.camera.orthographicSize;
		float horExtent = verExtent * Screen.width / Screen.height;
		// float camera_width = Screen.width/2;
		minX = -map.renderer.bounds.size.x/ 2 + horExtent + map.transform.position.x;
		maxX = map.renderer.bounds.size.x/ 2 - horExtent + map.transform.position.x;


	}

	public void freeze(float xPos)
	{
		isFrozen = true;
		frozenPos = xPos;
	}

	public void defrost()
	{
		isFrozen = false;
	}

	// Update is called once per frame
	void Update () {
		playerObj = GameObject.FindGameObjectWithTag (Globals.playerTag);
		if (playerObj) 
		{
			target = GameObject.FindGameObjectWithTag (Globals.playerTag).transform;
			if (target)
			{
				Vector3 point = camera.WorldToViewportPoint(target.position);
				Vector3 delta = target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)); //(new Vector3(0.5, 0.5, point.z));
				Vector3 destination = transform.position + delta;
				destination.y = transform.position.y;
				// clamp x 
				destination.x = Mathf.Clamp(destination.x, minX, maxX);
				if(isFrozen)
					destination.x = frozenPos;
				transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
			}
		}
	}
}
