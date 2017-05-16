using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePursue : FSMState
{
  private GameObject target;
  
  public override void Reason(GameObject player, GameObject npc)
  {
    throw new System.NotImplementedException();
  }

  public override void Act(GameObject player, GameObject npc)
  {
    GolemController agent = npc.GetComponent<GolemController>();


  }
}
