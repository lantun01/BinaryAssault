using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



[RequireComponent(typeof(BoxCollider2D))]
public class EnemyWaveManager : MonoBehaviour
{
    public List<EnemyWave> waves;
    public UnityEvent startWave;
    public UnityEvent endWave;
    private int cantidadWaves;
    private int currentWave = 0;
    private BoxCollider2D boxCollider;
    private Pooler pooler;

    private void Awake()
    {
        cantidadWaves = waves.Count;
        pooler = Pooler.instance;
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer==10)
        {
        startWave?.Invoke();
        SpawnNextWave();
        boxCollider.enabled = false;
        EnemyManager.instance.StartEncounter();
        }
    }

    public void SpawnNextWave()
    {
        if (currentWave>=cantidadWaves)
        {
            endWave?.Invoke();
            return;
        }

        waves[currentWave].Activate(this, pooler);
        currentWave++;
    }

}
