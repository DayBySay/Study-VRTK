using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public AudioClip seDamage;
	public AudioSource audio;
	bool isInvincible = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	 void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "WeaponSwordEnemy")
		{	
			if (isInvincible) {
				return;
			}

			damage();
		}
	}

	void damage() {
			audio.PlayOneShot(seDamage, 0.1f);
			isInvincible = true;
			Invoke("clearInvincible", 2.0f);
	}

	void clearInvincible() {
		isInvincible = false;
	}
}