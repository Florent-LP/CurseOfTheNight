using UnityEngine;
using UnityEngine.AI; //NavMeshAgent
using System.Collections;

public class MoveTo : MonoBehaviour {

	public Transform[] points;
  public bool isWalking = true;
	private int destPoint = 0;
	private NavMeshAgent agent;


	void Start () 
	{
		agent = GetComponent<NavMeshAgent>();
	  if (isWalking == true)
	  {
	    GetComponent<Animator>().SetBool("isWalking", true);
	    //agent.autoBraking = false;//the agent doen't slow down while reaching points
	    GoToNextPoint();
	  }
	}

	void GoToNextPoint()
	{
		if (points.Length == 0)
			return;
		agent.destination = points [destPoint].position;
		destPoint = (destPoint + 1) % points.Length;
	}

	void LateUpdate()
	{
		if (isWalking) 
		{
			if (GetComponent<Animator> ().GetBool("isAlive") && GetComponent<Animator> ().GetBool("isWalking") && !GetComponent<Animator> ().GetBool("isAlerted")) 
			{
				if (agent.remainingDistance < 0.75f)

					GoToNextPoint ();
			}
		}

	}
		
}