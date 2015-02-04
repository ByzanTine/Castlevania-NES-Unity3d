using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FetchSubWeapon : MonoBehaviour {
	private SubWeaponManager subWeapon;
	private Image image;
	// Use this for initialization
	void Start () {
		subWeapon = GameObject.FindGameObjectWithTag ("Player").GetComponent<SubWeaponManager> ();
		image = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!subWeapon) return;
		GameObject gb = subWeapon.getSubWeaponObject ();
		if (gb) {
			SpriteRenderer sprite = subWeapon.getSubWeaponObject ().GetComponent<SpriteRenderer> ();
			image.sprite = sprite.sprite;
			
			image.color = new Color(255, 255, 255, 255);
		}
		else {
			image.color = new Color(0, 0, 0, 255); // black off
		}
	}
}
