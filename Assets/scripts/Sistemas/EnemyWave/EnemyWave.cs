using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyWave: IPooleableCaller
{
    [SerializeField]
    public List<EnemySpawn> enemigos;
    public Color color = Color.red;
    public bool show;
    private Pooleable currentEnemy;
    private int defeatedEnemies;
    private int totalEnemy;
    private EnemyWaveManager manager;

    
    

    internal void Activate(EnemyWaveManager manager, Pooler pool)
    {
        this.manager = manager;
        totalEnemy = enemigos.Count;
        for (int i = 0; i < totalEnemy; i++)
        {
          currentEnemy = pool.SpawnObjeto(enemigos[i].enemigo);
          currentEnemy.Reiniciar();
          currentEnemy.Subscribir(this);
          currentEnemy.transform.position = enemigos[i].spawnPosition;
        }
    }

    private void EliminarEnemigo()
    {
        defeatedEnemies++;
        if (defeatedEnemies>=totalEnemy)
        {
            manager.SpawnNextWave();
        }
    }

    public void OnCall()
    {
        EliminarEnemigo();
    }
}


[System.Serializable]
public class EnemySpawn
{
    [SerializeField]
    [DraggablePoint]
    public Vector3 spawnPosition;
    [SerializeField]
    public Enemigo enemigo;
}