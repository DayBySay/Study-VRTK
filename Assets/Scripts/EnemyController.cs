using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (NavMeshAgent))]
[RequireComponent(typeof (ThirdPersonCharacter))]

public class EnemyController : MonoBehaviour {

	[SerializeField, HideInInspector] NavMeshAgent agent;
	[SerializeField, HideInInspector] Animator animator;
	public ThirdPersonCharacter character { get; private set; }
	public Transform target;
	bool attackable;

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		character = GetComponent<ThirdPersonCharacter>();
		animator = GetComponent<Animator>();

		agent.updateRotation = false;
		agent.updatePosition = true;
	}

	 private void Update() 	{
		animator.SetFloat("Speed", agent.velocity.magnitude);

		 if (target != null)
		 agent.SetDestination(target.position);

		if (!attackable) {
			character.Move(agent.desiredVelocity, false, false);
			agent.Resume();
		} else {
			character.Move(Vector3.zero, false, false);
			agent.Stop();
		}
	}


	 public void SetTarget(Transform target) 
	 {
		 this.target = target;
	}

	 void OnTriggerEnter(Collider other) {
		 	if (other.gameObject.tag == "Player") {
				attackable = true;
				animator.SetBool("Attackable", attackable);
			 }
	 }


	 void OnTriggerExit(Collider other) {
		 	if (other.gameObject.tag == "Player") {
				attackable = false;
				animator.SetBool("Attackable", attackable);
			 }
	 }
}