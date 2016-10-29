using UnityEngine;
using System.Collections;

public class AudiorController : MonoBehaviour {
    public AudioClip se;

	// Use this for initialization
	void Start () {
	        GetComponent<AudioSource>().PlayOneShot(se);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
