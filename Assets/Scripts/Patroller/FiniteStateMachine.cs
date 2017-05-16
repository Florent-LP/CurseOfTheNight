using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour
{
  private State previousState;
  private State currentState;

  public FiniteStateMachine(State previousState, State currentState)
  {
    this.previousState = previousState;
    this.currentState = currentState;
  }

  public void changeState(State newState)
  {
    previousState = currentState;
    currentState.exit();
    currentState = newState;
    currentState.enter();
  }
}
