using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    public GameEvent Evento;
    public UnityEvent response;

    private void OnEnable()
    {
        Evento.RegisterListener(this);
    }

    private void OnDisable()
    {
        Evento.UnregisterListener(this);
    }

    public void OnEventRaised()
    {
        response.Invoke();
    }
}
