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
	bool isDead = false;
	public AudioSource audio;
	public AudioClip seDeath;
	public AudioClip seDamage;
	bool isInvincible = false;

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		character = GetComponent<ThirdPersonCharacter>();
		animator = GetComponent<Animator>();

		agent.updateRotation = false;
		agent.updatePosition = true;
	}

	 private void Update() 	{
		 if (isDead)
		 {
			 return;
		 }

		 if (life <= 0)
		 {
			character.Move(Vector3.zero, false, false);
			animator.SetFloat("Speed", 0);
			agent.Stop();
			 animator.SetTrigger("Death");
			 Invoke("Death", 10.0f);
			 isDead = true;
			 audio.PlayOneShot(seDeath, 1.0f);
			 return;
		 }

		 float distance = getDistance(this.gameObject, target.gameObject);
		if (distance > 0.5) {
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
			 damage();
		 }
	 }

	 void damage() {
		 isInvincible = true;
		animator.SetTrigger("Hit");
		audio.PlayOneShot(seDamage, 1.0f);
		life -= 1;
		Invoke("clearInvincible", 2.0f);
	 }

	 void clearInvincible() {
		 isInvincible = false;
	 }

	 void Death() {
		 Destroy(this.gameObject);	
		}
	}