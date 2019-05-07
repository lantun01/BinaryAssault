using System.Collections.Generic;
using UnityEngine;
using static DialogManager;

public class Dialogo : MonoBehaviour
{
   [TextArea]
   public string[] dialogos;

   [HideInInspector] public bool iniciado;
   List<Dialogo> _dialogos = new List<Dialogo>();
   
   public void Iniciar()
   {
      if (!iniciado)
         instance.StartDialogue(this , transform.position + Vector3.up);
      else
         instance.NextLine();
   }

   public string this[int index] => dialogos[index];
}
