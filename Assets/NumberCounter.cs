using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NumberCounter : MonoBehaviour
{
   [SerializeField] private IntVariable number;
   [SerializeField] private TextMeshProUGUI textMesh;

   public void Actualizar()
   {
      textMesh.text = number.variable.ToString();
   }

}
