using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //NavMeshAgent

//Appliquer sur les ennemis
[RequireComponent(typeof(AudioSource))]
public class EnemyHealth : MonoBehaviour
{
	public int maxHealth = 100;
	public int currentHealth;

	public ParticleSystem blood;

	//public float tempsInvincibilite = 1.042f;

	private bool isDead = false;
	private bool invincibility = false;

	private Animator anim;
	private NavMeshAgent agent;
	private AudioSource audio;

	//private CapsuleCollider golemCol;

	//private Vector3 deadPosition;
	//private Vector3 alivePosition;

	void Awake()
	{
		currentHealth = maxHealth;
		anim = this.GetComponent<Animator>();
		agent = GetComponent<NavMeshAgent>();
		AudioSource[] audios = GetComponents<AudioSource>();
		audio = audios[1];
		//golemCol = this.GetComponent<CapsuleCollider> ();
		//alivePosition = golemCol.center;
		//deadPosition = new Vector3(golemCol.center.x, golemCol.center.y + 0.5f, golemCol.center.z);
	}

	public IEnumerator TakeDamage(int amount, float timeInvincibility)
	{
		if (!invincibility)
		{
			anim.SetBool ("isHit", true);
			currentHealth -= amount; //Réduction de PV

			if (currentHealth <= 0 && !isDead)
				Die ();
			else 
			{
				//Temps d'invincibilité après s'être fait touché
				invincibility = true;
				yield return new WaitForSeconds (timeInvincibility);
				invincibility = false;
				anim.SetBool ("isHit", false);
			}
		}
	}

	public void playBlood()
	{
		blood.Play ();
	}

	public void Die()
	{
		audio.Play ();
		anim.SetBool ("isDead", true);
		//golemCol.center = deadPosition;
		anim.SetBool ("isWalking", false);
		GetComponent<MoveTo> ().enabled = false;
		GetComponent<GolemPatrol> ().enabled = false;
		GetComponent<EnemyAttack> ().enabled = false;
		GetComponent<EnemyHealth> ().enabled = false;
		GetComponent<FieldOfViewPatroller> ().enabled = false;
		GetComponent<FieldOfAttackPatroller> ().enabled = false;
	  this.gameObject.transform.Find("Indicator Sphere").gameObject.SetActive(false);
    isDead = true;
		Statistics.nbPlayerKills++;
    this.gameObject.transform.Find("Indicator Sphere").GetComponent<Renderer>().material.color = Color.black;
    agent.enabled = false;
	}
}
