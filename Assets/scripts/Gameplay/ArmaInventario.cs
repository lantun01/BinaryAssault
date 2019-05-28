using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmaInventario
{
   public ArmaData data;
   public int municion;

   public bool TieneMunicion()
   {
      if (data.municionInfinita)
      {
         return true;
      }
      return municion > 0;
   }

   public ArmaInventario(ArmaData data)
   {
      this.data = data;
      municion = data.municion;
   }
}
