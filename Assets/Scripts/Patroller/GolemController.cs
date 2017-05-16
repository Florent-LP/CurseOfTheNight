using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GolemController : MonoBehaviour
{
  public Transform[] path;
  public NavMeshAgent agent;

  private bool hasHeardSomething;
  private List<Transform> visibleTargets;
  private FSMSystem fsm;
  private Animator anim;
  

  // Use this for initialization
  void Start () {
		MakeFSM();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

  void MakeFSM()
  {
    //StatePatrol statePatrol = new StatePatrol(path,GetComponent<NavMeshAgent>()
    //  ,visibleTargets,hasHeardSomething);
    //statePatrol.AddTransition(Transition.PatrolLooking, StateID.StateLooking);
    //statePatrol.AddTransition(Transition.PatrolPursue, StateID.StatePursue);


  }

  public void SetTransition(Transition t)
  {
    fsm.PerformTransition(t);
  }
}
