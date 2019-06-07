using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



[RequireComponent(typeof(BoxCollider2D))]
public class EnemyWaveManager : MonoBehaviour
{
    public List<EnemyWave> waves;
    public UnityEvent startWave;
    public UnityEvent endWave;
    public GameEvent endWaveEvent;
    private int cantidadWaves;
    private int currentWave = 0;
    private BoxCollider2D boxCollider;
    private Pooler pooler;

    private void Awake()
    {
        cantidadWaves = waves.Count;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        pooler = Pooler.instance;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer==10)
        {
            StartEncounter();
        }
    }

    public void StartEncounter()
    {
        startWave?.Invoke();
        SpawnNextWave();
        boxCollider.enabled = false;
        EnemyManager.instance.StartEncounter();
    }

    private void OnValidate()
    {
        if (!boxCollider)
        {
        boxCollider = GetComponent<BoxCollider2D>();
        boxCollider.isTrigger = true;
        }
    }

    public void SpawnNextWave()
    {
        if (currentWave>=cantidadWaves)
        {
            endWave?.Invoke();
            endWaveEvent?.Raise();
            return;
        }

        waves[currentWave].Activate(this, pooler);
        currentWave++;
    }

}
