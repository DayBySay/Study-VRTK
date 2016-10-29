using UnityEngine;
using System.Collections;

public class WeaponController : MonoBehaviour {
GameObject parent;
SteamVR_TrackedObject trackedObject;
	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject;
		trackedObject = parent.GetComponent<SteamVR_TrackedObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void vive() {
 		var device = SteamVR_Controller.Input((int) trackedObject.index);
		device.TriggerHapticPulse(500);
	}
}
