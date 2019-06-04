using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



[System.Serializable]
public class UnityProyectilEvent : UnityEvent<Proyectil>
{

}

public class Atacable : MonoBehaviour
{
    public UnityProyectilEvent RecibirGolpe;
}
