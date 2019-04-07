using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    private void Awake()
    {
        instance = this;
    }
    public List<Enemigo> enemigos;

    public Enemigo GetNerbyEnemy(Vector3 pos)
    {
        if (enemigos.Count!=0)
        {
            int indice = 0;
            float min = (enemigos[0].transform.position - pos).sqrMagnitude;
            for (int i = 0; i < enemigos.Count; i++)
            {
                float distancia = (enemigos[i].transform.position - pos).sqrMagnitude;
                if (distancia < min)
                {
                    min = distancia;
                    indice = i;
                }
            }

            return enemigos[indice];
        }
        else
        {
            return null;
        }
    }
}
