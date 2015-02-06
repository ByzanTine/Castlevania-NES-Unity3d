using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

	private static InputManager instance;
	private InputManager() {}
	public static InputManager Instance
	{
		get
		{
			if (instance == null)
				instance = GameObject.FindObjectOfType(typeof(InputManager)) as  InputManager;
			return instance;
		}
	}
	// Event Handler
	public delegate void OnKeyPress();

	// Map to classical control
	// single button control 
	public event OnKeyPress OnKeyPress_Right;
	public event OnKeyPress OnKeyUp_Right;
	public event OnKeyPress OnKeyPress_Left;
	public event OnKeyPress OnKeyUp_Left;
	public event OnKeyPress OnKeyDown_Down;
	public event OnKeyPress OnKeyPress_Down;
	public event OnKeyPress OnKeyUp_Down;
	public event OnKeyPress OnKeyPress_Up;
	public event OnKeyPress OnKeyUp_Up;
	public event OnKeyPress OnKeyDown_B;
	public event OnKeyPress OnKeyDown_A;
	public event OnKeyPress OnKeyDown_G;
	// chord
	public event OnKeyPress OnKeyDown_Up_And_B;

	public bool disableControl = false;

	void Awake ()
	{
		// Instance control
		if (instance != null && instance != this) 
		{
			Destroy (this.gameObject);
			return;
		}
		else 
		{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}


	// Handle our Ray and Hit
	void Update () 
	{
		if (disableControl)
			return;
		// chord detection, first priority 
		if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) &&
		    (Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown (KeyCode.Period))) {
			OnKeyDown_Up_And_B();
			return;
		}

		if (Input.GetKeyDown (KeyCode.Z) || Input.GetKeyDown (KeyCode.Comma)) {
			OnKeyDown_A();
		}
		if (Input.GetKey (KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
			OnKeyDown_Down();
		}
		if (Input.GetKeyUp (KeyCode.S) || Input.GetKeyUp (KeyCode.DownArrow)) {
			OnKeyPress_Down();
		}
		if (Input.GetKeyUp (KeyCode.S) || Input.GetKeyUp (KeyCode.DownArrow)) {
			OnKeyUp_Down();
		}
		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
			OnKeyPress_Right();
		}
		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
			OnKeyPress_Left();
		}
		if (Input.GetKeyUp (KeyCode.RightArrow) || Input.GetKeyUp (KeyCode.D)) {
			OnKeyUp_Right();
		}
		if (Input.GetKeyUp (KeyCode.LeftArrow) || Input.GetKeyUp (KeyCode.A)) {
			OnKeyUp_Left();
		}
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
			OnKeyPress_Up();
		}
		if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) {
			OnKeyUp_Up();
		}

		if (Input.GetKeyDown (KeyCode.X) || Input.GetKeyDown (KeyCode.Period)) {
			OnKeyDown_B();
		}
		if (Input.GetKeyDown (KeyCode.G)) {
			OnKeyDown_G();
		}

	}
}
