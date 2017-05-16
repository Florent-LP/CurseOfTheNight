using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatePatrolling :  State {

  private NavMeshAgent agent { get; set; }
  private Transform[] points { get; set; }
  private Animator anim;
  private int destPoint = 0;

  public StatePatrolling(Animator anim, int destPoint, NavMeshAgent agent, Transform[] points)
  {
    this.anim = anim;
    this.destPoint = destPoint;
    this.agent = agent;
    this.points = points;
  }

  void GoToNextPoint()
  {
    if (points.Length == 0)
      return;
    agent.destination = points[destPoint].position;
    destPoint = (destPoint + 1) % points.Length;
  }

  public void enter()
  {
    anim.SetBool("isWalking", true);
    GoToNextPoint();
  }

  public void execute()
  {
    if (anim.GetBool("isAlive") && anim.GetBool("isWalking") && !anim.GetBool("isAlerted"))
    {
      if (agent.remainingDistance < 0.75f)
        GoToNextPoint();
    }
  }

  public void exit()
  {
    
  }
}
