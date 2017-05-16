using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Appliquer sur les ennemis
[RequireComponent(typeof(AudioSource))]
public class EnemyAttack : MonoBehaviour {

	public int power = 5;
	public float tempsPunch = 0.586f; //Temporaire -> remplacer par temps animation
	private float nextPunch; //Temps auquel le prochain puch pourra se faire

	private GameObject target;
	private Animator anim;

	private PlayerHealth playerHP;
    private GameObject parent;
	private GolemPatrol patrol;
	private AudioSource audio;
	int countAudio = 1; //Dégueu mais pas trouvé d'autres solutions
	int i=0;

	public List<Transform> visibleTargets = new List<Transform> ();

	// Use this for initialization
	void Start ()
	{
		nextPunch = 0.0f;
		anim = GetComponent<Animator>();
		patrol = GetComponent<GolemPatrol>(); 
		visibleTargets = gameObject.GetComponent<FieldOfAttackPatroller> ().visibleTargets;
		AudioSource[] audios = GetComponents<AudioSource>();
		audio = audios[0];
  }

	// Update is called once per frame
	void Update ()
	{
		//Etat de l'ennemi
		if (!anim.GetBool ("isDead") && !anim.GetBool("isHit"))
		{
			if (visibleTargets.Count > 0) 
			{
				target = visibleTargets [0].gameObject;
			} 
			else 
			{
				anim.SetBool ("isAttacking", false);
				target = null;
			}

			if (target != null && !target.GetComponent<Animator> ().GetBool ("isDying")) 
			{
				playerHP = target.GetComponent<PlayerHealth> ();
				attack ();
			}
		}
	}

	public void attack()
	{
		if (Time.time > nextPunch) 
		{
			nextPunch = Time.time + tempsPunch;
			anim.SetBool ("isAttacking", true);
			StartCoroutine (playerHP.TakeDamage (power, tempsPunch));

			countAudio++;
			if (countAudio % 2 == 0) 
			{
				audio.Play ();
				Debug.Log (i);
				i++;
				countAudio = 0;
			}
		}
		else
		{
			anim.SetBool ("isAttacking", false);
		}
	}	
}
