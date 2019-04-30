using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Pooleable: MonoBehaviour
{
    public abstract void Activar();
    public abstract void Reiniciar();
    public abstract void Subscribir(IPooleableCaller caller);
    public abstract void Desactivar();
}
