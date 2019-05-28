using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using static DialogManager;

public class Dialogo : MonoBehaviour
{
   [TextArea]
   public string[] dialogos;

   [HideInInspector] public bool iniciado;
   List<Dialogo> _dialogos = new List<Dialogo>();
   public UnityEvent startDialogue;
   public UnityEvent endDialog;
  




   public void Iniciar()
   {
       
      //Subscribir listener a dialogo

      if (!iniciado)
      {
          iniciado = true;
          startDialogue?.Invoke();
          instance.endDialogAction += EndDialog;
          instance.StartDialogue(this, transform.position + Vector3.up);
      }
   else
   instance.NextDialog();
   }

   public void EndDialog()
   {
       endDialog?.Invoke();
       instance.endDialogAction -= EndDialog;
   }

   public string this[int index] => dialogos[index];

  

 
}
