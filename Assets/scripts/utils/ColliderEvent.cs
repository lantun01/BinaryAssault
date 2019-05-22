using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ColliderEvent : MonoBehaviour
{
  public UnityEvent OnCollision;

  private void OnTriggerEnter2D(Collider2D other)
  {
    OnCollision?.Invoke();
  }
}
