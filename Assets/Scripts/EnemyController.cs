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
	public int life;
	bool isDead = false;
	public AudioSource audio;
	public AudioClip seDeath;
	public AudioClip seDamage;
	public AudioClip seSpone;
	bool isInvincible = false;
	bool isActive = false;

	void Start()
	{
		agent = GetComponent<NavMeshAgent>();
		character = GetComponent<ThirdPersonCharacter>();
		animator = GetComponent<Animator>();

		agent.updateRotation = false;
		agent.updatePosition = true;
		spone();
	}

	 private void Update() 	{
		 if (isDead || !isActive)
		 {
			 return;
		 }

		 if (life <= 0)
		 {
			character.Move(Vector3.zero, false, false);
			animator.SetFloat("Speed", 0);
			agent.Stop();
			 animator.SetTrigger("Death");
			 Invoke("Death", 7.0f);
			 isDead = true;
			 audio.PlayOneShot(seDeath, 1.0f);
			 return;
		 }

		 float distance = getDistance(this.gameObject, target.gameObject);
		if (distance > 2) {
			 agent.SetDestination(target.position);
			character.Move(agent.desiredVelocity, false, false);
			animator.SetFloat("Speed", agent.velocity.sqrMagnitude);
			agent.Resume();
			animator.SetBool("Attackable", false);
		} else {
			character.Move(Vector3.zero, false, false);
			animator.SetFloat("Speed", 0);
			agent.Stop();
			animator.SetBool("Attackable", true);
		}
	}

	void spone() {
		Invoke("activate", 2.0f);
		audio.PlayOneShot(seSpone, 3.0f);
	}
	void activate() {
		isActive = true;	
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
			if (isInvincible) {
				return;
			}

			 damage();
		 }
	 }

	 void damage() {
		 isInvincible = true;
		animator.SetTrigger("Hit");
		audio.PlayOneShot(seDamage, 1.0f);
		life -= 1;
		Invoke("clearInvincible", 1.0f);
	 }

	 void clearInvincible() {
		 isInvincible = false;
	 }

	 void Death() {
		 Destroy(this.gameObject);	
		}
	}