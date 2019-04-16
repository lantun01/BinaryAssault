using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyWave 
{
    [SerializeField]
    public List<EnemySpawn> enemigos;
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