using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    public Dictionary<IPooleable, Queue<IPooleable>> pool = new Dictionary<IPooleable, Queue<IPooleable>>();
    public static Pooler instance;
    private IPooleable nuevoObj;
    private int defaultQuantity = 10;

    private void Awake()
    {
        instance = this;
    }

    public void CrearPool(IPooleable pooleable, int cantidad)
    {
        if (!pool.ContainsKey(pooleable))
        {
            Queue<IPooleable> qpool = new Queue<IPooleable>();
            for (int i = 0; i < cantidad; i++)
            {
                IPooleable obj = Instantiate(pooleable);
                obj.Desactivar();
                qpool.Enqueue(obj);

            }
            pool.Add(pooleable, qpool);
        }
        else
        {
            GrowPool(pooleable, cantidad);
        }
      
    }

  
    private void GrowPool(IPooleable pooleable, int cantidad)
    {
        for (int i = 0; i < cantidad; i++)
        {
            IPooleable obj = Instantiate(pooleable);
            obj.Desactivar();
            pool[pooleable].Enqueue(obj);
        }
    }

    public IPooleable SpawnObjeto(IPooleable obj)
    {
        if (!pool.ContainsKey(obj))
        {
            CrearPool(obj, defaultQuantity);
        }
        nuevoObj = pool[obj].Dequeue();
        nuevoObj.Activar();
        pool[obj].Enqueue(nuevoObj);
        return nuevoObj;
    }
}
