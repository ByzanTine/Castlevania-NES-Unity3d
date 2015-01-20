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
	public LayerMask collideLayer;// Only collide with the colliders in this layer
	// Event Handler
	public delegate void OnKeyPress();

	// Map to classical control
	public event OnKeyPress OnKeyPress_Right;
	public event OnKeyPress OnKeyPress_Left;
	public event OnKeyPress OnKeyDown_Down;
	public event OnKeyPress OnKeyUp_Down;
	public event OnKeyPress OnKeyPress_Up;
	public event OnKeyPress OnKeyDown_B;
	public event OnKeyPress OnKeyDown_A;
	// Handle our Ray and Hit
	void Update () 
	{
		if (Input.GetKeyDown (KeyCode.Z) || Input.GetKeyDown (KeyCode.Comma)) {
			OnKeyDown_A();
		}
		if (Input.GetKeyDown (KeyCode.X) || Input.GetKeyDown (KeyCode.Period)) {
			OnKeyDown_B();
		}
		if (Input.GetKeyDown (KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
			OnKeyDown_Down();
		}
		if (Input.GetKeyUp (KeyCode.S) || Input.GetKeyUp (KeyCode.DownArrow)) {
			OnKeyUp_Down();
		}

	}
}
