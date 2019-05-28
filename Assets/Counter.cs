using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Counter : MonoBehaviour,IUpdateable
{
    [SerializeField] private float count;
    private float current;

    public UnityEvent OnCountReach;
    // Start is called before the first frame update

    private void Start()
    {
        Subscribir();
    }

    private void OnDisable()
    {
        UpdateManager.instance.Unsubscribe(this);
    }

    private void OnEnable()
    {
        UpdateManager.instance.Subscribe(this);
    }

    public void CustomUpdate()
    {
        current += Time.deltaTime;
        if (current>count)
        {
            current = 0;
            OnCountReach?.Invoke();
        }
    }

    public void Subscribir()
    {
       UpdateManager.instance.Subscribe(this);
    }
}
