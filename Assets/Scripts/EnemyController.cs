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
	int life = 10;


	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		character = GetComponent<ThirdPersonCharacter>();
		animator = GetComponent<Animator>();

		agent.updateRotation = false;
		agent.updatePosition = true;
	}

	 private void Update() 	{
		 if (life <= 0)
		 {
			character.Move(Vector3.zero, false, false);
			animator.SetFloat("Speed", 0);
			agent.Stop();
			 animator.SetTrigger("Death");
			 return;
		 }

		 float distance = getDistance(this.gameObject, target.gameObject);
		if (distance > 2) {
			 agent.SetDestination(target.position);
			character.Move(agent.desiredVelocity, false, false);
			animator.SetFloat("Speed", agent.velocity.magnitude);
			agent.Resume();
			animator.SetBool("Attackable", false);
		} else {
			character.Move(Vector3.zero, false, false);
			animator.SetFloat("Speed", 0);
			agent.Stop();
			animator.SetBool("Attackable", true);
		}
	}


	 public void SetTarget(Transform target) 
	 {
		 this.target = target;
	}

	 float getDistance(GameObject from, GameObject to) {
		 return Vector3.Distance(from.transform.position, to.transform.position);
	 }

	 void OnTriggerEnter(Collider other) {
		 if (other.gameObject.tag == "Weapon") {
			animator.SetTrigger("Hit");
			life -= 1;
		 }
	 }
}