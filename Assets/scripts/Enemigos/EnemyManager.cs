using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public Transform targeCircle;
    private Enemigo target;

    private void Awake()
    {
        instance = this;
    }
    public List<Enemigo> enemigos;

    public Enemigo GetNerbyEnemy(Vector3 pos)
    {
        if (enemigos.Count!=0)
        {
            targeCircle.gameObject.SetActive(true);
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
            target = enemigos[indice];
            targeCircle.transform.position = target.TargetPos();
            return target;
        }
        else
        {
            target.gameObject.SetActive(false);
            return null;
        }
    }
}
