using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatePatrol : FSMState
{
  public Transform[] points;

  private List<Transform> visibleTargets;
  private bool hasHeardSomething;
  private int destPoint = 0;

  public StatePatrol(Transform[] points,  
    List<Transform> visibleTargets, bool hasHeardSomething)
  {
    this.points = points;
    this.visibleTargets = visibleTargets;
    this.hasHeardSomething = hasHeardSomething;
  }

  public override void Reason(GameObject player, GameObject npc)
  {
    if (visibleTargets.Count != 0)
    {
      npc.GetComponent<GolemController>().SetTransition(Transition.PatrolPursue);
    }
    if (hasHeardSomething)
    {
      npc.GetComponent<GolemController>().SetTransition(Transition.PatrolLooking);
    }
  }

  public override void Act(GameObject player, GameObject npc)
  {
    if (points.Length == 0)
      return;
    //agent.destination = points[destPoint].position;
    destPoint = (destPoint + 1) % points.Length;
  }
}
