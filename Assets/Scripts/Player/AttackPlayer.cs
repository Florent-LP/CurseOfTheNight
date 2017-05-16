using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AttackPlayer : MonoBehaviour {

	public int degatPunch = 20;
	public int degatKick = 40;

	public int bloodAmount = 100;

	public float tempsPunch = 1.042f;
	public float tempsKick = 0.833f;

	private float nextAttack;

    private GameObject target;
    private Animator anim;
	private List<Transform> visibleTargets = new List<Transform> ();
	private AudioSource audio;

    private EnemyHealth enemyHP;
	private PlayerBlood pb;
  private FieldOfViewPatroller fov;

    // Use this for initialization
    void Start ()
	{
		nextAttack = 0.0f;
		AudioSource[] audios = GetComponents<AudioSource>();
		pb = GetComponent<PlayerBlood> ();
		audio = audios[0];
	}
	
	// Update is called once per frame
	void Update ()
    {
		visibleTargets = gameObject.GetComponent<FieldOfViewPlayer> ().visibleTargets;
		if (visibleTargets.Count > 0) {
			target = visibleTargets [0].gameObject;
		}
        if (target != null)
		{
			if (Time.time > nextAttack) 
			{
				enemyHP = target.GetComponent<EnemyHealth> ();
        fov = target.GetComponent<FieldOfViewPatroller>();
        if (Input.GetMouseButtonDown (0) && GetComponent<Animator> ().GetBool ("isSneakyAndNear")) //Touche d'attaque
                {
                    audio.Play();
                    if (Vector3.Dot (target.transform.forward, this.transform.forward) > 0) //L'attaque est dans le dos
					{
						pb.addBlood (bloodAmount);
						enemyHP.playBlood ();
						StartCoroutine (enemyHP.TakeDamage (enemyHP.maxHealth, 0));
					} 
					else 
					{
						StartCoroutine (enemyHP.TakeDamage (degatPunch, tempsPunch));
					}
					nextAttack = Time.time + tempsPunch;
				}
				if (Input.GetMouseButtonDown (2) && GetComponent<Animator> ().GetBool ("isSneakyAndNear")) //Touche d'attaque de kick
				{ 
					if (Vector3.Dot (target.transform.forward, this.transform.forward) > 0) //L'attaque est dans le dos
					{ 
						StartCoroutine (enemyHP.TakeDamage (enemyHP.maxHealth, 0));
            fov.crappyFix();
					} 
					else 
					{
						StartCoroutine (enemyHP.TakeDamage (degatKick, tempsKick));
					}
					nextAttack = Time.time + tempsKick;
				}
			}
        }
    }
}
