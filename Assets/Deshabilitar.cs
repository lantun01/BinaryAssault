using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deshabilitar : MonoBehaviour
{
  public void Active()
  {
    gameObject.SetActive(true);
  }

  public void Desactivar()
  {
    gameObject.SetActive(false);
  }
}
