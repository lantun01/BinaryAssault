using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.Events;

public class Interactuable : MonoBehaviour
{
   [SerializeField]
   private UnityEvent interactuar;

   public void Interactuar()
   {
      interactuar?.Invoke();
   }

   private void Update()
   {
      if(Input.GetKeyDown(KeyCode.A))
         Interactuar();
   }
}
