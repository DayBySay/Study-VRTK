using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	[SerializeField, HideInInspector] NavMeshAgent agent;
	[SerializeField, HideInInspector] Animator animator;

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		animator = GetComponent<Animator>();
	}

	void Update () 
	{
		animator.SetFloat("Speed", agent.velocity.magnitude);
	}

	 void OnTriggerEnter(Collider other) {
		 	if (other.gameObject.tag == "Player") {
				 agent.Stop();
			 }
	 }

}
