using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IPooleable:MonoBehaviour
{
    public abstract void Activar();
    public abstract void Reiniciar();
    public abstract void Subscribir();
    public abstract void Desactivar();
}
