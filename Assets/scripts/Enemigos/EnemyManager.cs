using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;
    public Transform targetCircle;
    private Enemigo target;
    public List<Enemigo> enemigos;
    private bool enEncuentro;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

    }


    public void AddEnemigo(Enemigo enemigo)
    {
        enemigos.Add(enemigo);
    }

    public void RemoveEnemigo(Enemigo enemigo)
    {
        if (enemigo)
        {
        enemigos.Remove(enemigo);
        }
    }

    public void StartEncounter()
    {
        targetCircle.gameObject.SetActive(true);
        enEncuentro = true;
    }

    public Enemigo GetNerbyEnemy(Vector3 pos)
    {
        if (enEncuentro)
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
            target = enemigos[indice];
            targetCircle.transform.position = target.TargetPos();
            return target;
        }

        return null;
    }

    public void EndEncounter()
    {
        enEncuentro = false;
        targetCircle.gameObject.SetActive(false);
    }
}
