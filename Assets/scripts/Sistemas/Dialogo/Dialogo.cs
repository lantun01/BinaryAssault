using UnityEngine;
using static DialogManager;

public class Dialogo : MonoBehaviour
{
   [TextArea]
   public string[] dialogos;

   [HideInInspector] public bool iniciado;
   
   public void Iniciar()
   {
      if (!iniciado)
         instance.StartDialogue(this);
      else
         instance.NextLine();
   }
   public string this[int i] => dialogos[i];
}
