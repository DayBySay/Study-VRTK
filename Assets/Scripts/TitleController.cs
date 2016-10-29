using UnityEngine;
using System.Collections;

public class TitleController : VRTK.VRTK_InteractableObject {
	bool starting = false;
	public AudioSource audio;
	public AudioClip seDrawn;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (starting)
		{
			return;
		}

		if (this.IsGrabbed())
		{
			drawn();
		}
	}

	void drawn() {
		audio.PlayOneShot(seDrawn, 1.0f);
		starting = true;

		Invoke("startGame", 5.0f);
	}

	void startGame() {
		SteamVR_LoadLevel.Begin("Hunting");
	}
}
