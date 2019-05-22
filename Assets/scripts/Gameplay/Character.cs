using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField]
    private int saludMaxima;
    [SerializeField]
    protected float velocidad;
    private int _saludActual;
    public Rigidbody2D rb;
    public Vector2 mirada;
    public Vector3 dirMov;
    public UnityEvent OnMorir;
    [HideInInspector] protected bool invulnerable;
    public int saludActual { get=>_saludActual; set
        {
            if (value<_saludActual && invulnerable)
            {
                return;
            }
           
            if (value<0)
            {
                _saludActual = 0;
                OnMorir?.Invoke();
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
