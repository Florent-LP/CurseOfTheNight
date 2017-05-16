using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StatePursuing : State
{
  private GolemPatrol agent;
  private GameObject target;

  public StatePursuing(GolemPatrol agent, GameObject target)
  {
    this.agent = agent;
    this.target = target;
  }

  public void enter()
  {
    Debug.Log("Going into pursue mode");
    agent.alertStartTime = Time.time;
    agent.GetComponent<Animator>().SetBool("isWalking", false);
    agent.GetComponent<Animator>().SetBool("isAlerted", true);
    agent.agent.speed = agent.chaseSpeedCoeff * agent.walkSpeed;
  }

  public void execute()
  {
    agent.agent.destination = target.transform.position;
  }

  public void exit()
  {

  }
}
