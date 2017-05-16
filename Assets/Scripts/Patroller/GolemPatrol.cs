using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //NavMeshAgent

public class GolemPatrol : MonoBehaviour {

	public List<Transform> visibleTargets = new List<Transform> ();
	private GameObject target;
  private Renderer indicator;
  private FieldOfAttackPatroller fovAttack;
  [HideInInspector]
	public NavMeshAgent agent;
	public float golemAttackRange = 10.0f;

	public float walkSpeed;
	public float chaseSpeedCoeff = 2.0f;

	public float alertStartTime;
	public float safeStartTime;
	public int safeTime = 25;//doens't work ! Why ? Constants work but not this variable...

  // Use this for initialization
	void Start () 
	{
		agent = GetComponent<NavMeshAgent>();
		alertStartTime = 0;
		safeStartTime = 0;
		walkSpeed = agent.speed;
		//GetComponent<Animator> ().SetBool ("isWalking", false);
		GetComponent<Animator> ().SetBool ("isAlive", true);
		gameObject.GetComponent<TextMesh> ();
	  indicator = this.gameObject.transform.Find("Indicator Sphere").GetComponent<Renderer>();
	  fovAttack = this.GetComponent<FieldOfAttackPatroller>();
	}

	void ChasePlayer()
	{
		//Debug.Log ("CHASE YOU!");
		agent.destination = target.transform.position;
	}

  public void ChasePlayerFromSound(Vector3 position)
  {
    if (indicator != null ) indicator.material.color = Color.yellow;
    agent.destination = position;
  }

	void LateUpdate()
	{
	  if (!GetComponent<Animator>().GetBool("isDead"))
	  {
	    visibleTargets = gameObject.GetComponent<FieldOfViewPatroller>().visibleTargets;
	    //Debug.Log ("Number of targets is : " + visibleTargets.Count);
			if (!GetComponent<Animator>().GetBool("isAlerted") && GetComponent<Animator>().GetBool("isWalking") || !GetComponent<Animator>().GetBool("isAlerted") && !GetComponent<MoveTo>().enabled)
	    {
	      if (visibleTargets.Count > 0)
	      {
			    
	        Debug.Log("FOUND YOU!");
	        if (indicator != null) indicator.material.color = Color.red;
	        alertStartTime = Time.time;
	        GetComponent<Animator>().SetBool("isWalking", false);
	        GetComponent<Animator>().SetBool("isAlerted", true);
	        agent.speed = chaseSpeedCoeff * walkSpeed;
	        target = visibleTargets[0].gameObject;
	      }

	      else
	      {
	        alertStartTime = 0;
	        if (indicator != null) indicator.material.color = Color.green;
	      }
	      safeStartTime = 0;
	    }

	    if (GetComponent<Animator>().GetBool("isAlerted") && !GetComponent<Animator>().GetBool("isWalking"))
	    {
	      if (agent.remainingDistance < golemAttackRange && fovAttack.visibleTargets.Count == 0) //not efficient
	        ChasePlayer();
        else if(fovAttack.visibleTargets.Count > 0)
          indicator.material.color = Color.red;
        if ((visibleTargets.Count <= 0) && (safeStartTime <= 0))
	      {
	        //Debug.Log("Player SAFE begins!");
	        safeStartTime = Time.time;
	      }

	      float t = Time.time - safeStartTime; //problem here?

	      if (((int) t % 60 >= 5) && (safeStartTime > 0))
	      {
	        // >=safeTime doesn't work... why ?? 
	        //Debug.Log("Time is : " + Time.time);
	        //Debug.Log("safeStartTime is : " + safeStartTime);
	        //Debug.Log("safeTime calculated in seconds is : " + t%60);
	        safeStartTime = 0;
	        agent.speed = walkSpeed;
	        GetComponent<Animator>().SetBool("isAlerted", false);
					if (GetComponent<MoveTo> ().enabled) 
					{
						GetComponent<Animator>().SetBool("isWalking", true);
					}
					else	       
						GetComponent<Animator>().SetBool("isWalking", false);

	        //Debug.Log ("WHERE ARE YOU?!");
	      }
	    }
	  }
	}

	void Update() 
	{
		if (!GetComponent<Animator> ().GetBool ("isDead") && GetComponent<Animator>().GetBool("isDead")) 
		{
      
      if (alertStartTime>0) {
				float t = Time.time - alertStartTime;
				string minutes = ((int)t / 60).ToString ();
				string seconds = (t % 60).ToString ("F2");
			}
			if (safeStartTime>0) {
				//float t = Time.time - safeStartTime;
				//string minutes = ((int)t / 60).ToString ();
				//string seconds = (t % 60).ToString ("F2");
				//Debug.Log( "Safe Time (5sec) is " + minutes + ":" + seconds);
			}
		}
	}
}
