using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Evento")]
public class GameEvent : ScriptableObject
{
    private List<EventListener> listeners;

    public void Raise()
    {
        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].OnEventRaised();
        }
    }
    internal void RegisterListener(EventListener eventListener)
    {
        listeners.Add(eventListener);
    }

    internal void UnregisterListener(EventListener eventListener)
    {
        listeners.Remove(eventListener);
    }
}
