using UnityEngine;
using System.Collections;

public class EnemyWeaponController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Shield") {
			GameObject parent = transform.parent.parent.gameObject;
			parent.GetComponent<Animator>().SetTrigger("Repelled");
		}
	}
}
