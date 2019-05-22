using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Utils;

public class EmiterController : MonoBehaviour
{
    [SerializeField] private ParticleSystem particle;
    private ParticleSystem.EmissionModule _emissionModule;
    public UnityEvent startEmiting;
    public UnityEvent endEmiting;
    private float duration;

    private void OnValidate()
    {
        particle = GetComponent<ParticleSystem>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _emissionModule = particle.emission;
        duration = particle.main.duration;
    }

    public void Play()
    {
        _emissionModule.enabled = true;
        particle.Play();
        startEmiting?.Invoke();
        StartCoroutine(Corroutines.Wait(duration, null, ()=>endEmiting?.Invoke()));
    }
}
