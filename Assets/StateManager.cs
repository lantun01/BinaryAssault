using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
  [SerializeField] private GameEvent playingState;
  [SerializeField] private GameEvent waitingState;

  public void RaisePlaying()
  {
    playingState.Raise();
  }

  public void RaiseWaiting()
  {
    waitingState.Raise();
  }
}
