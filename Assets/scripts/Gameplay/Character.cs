using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private int saludMaxima;
    [SerializeField]
    protected float velocidad;
    private int _saludActual;
    protected int saludActual { get=>_saludActual; set
        {
            if (value<0)
            {
                _saludActual = 0;
            }
            else if (value>saludMaxima)
            {
                _saludActual = saludMaxima;
            }
            else
            {
                _saludActual = value;
            }

            
        }
    }

    protected float GetSaludRatio()
    {
        return (float)saludActual / saludMaxima;
    }

    protected void Inicializar()
    {
        saludActual = saludMaxima;
    }
}
