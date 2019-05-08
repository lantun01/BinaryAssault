using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    public Dictionary<Pooleable, Queue<Pooleable>> pool = new Dictionary<Pooleable, Queue<Pooleable>>();
    public List<PreloadedObject> preloadedObjects;
    public static Pooler instance;
    private Pooleable nuevoObj;
    private int defaultQuantity = 10;

    private void Awake()
    {
        if (instance== null)
        {
            instance = this;
           
        
        }
        else
        {
            Destroy(this);
        }
        
        
    }

    private void Start()
    {
        for (int i = 0; i < preloadedObjects.Count; i++)
        {
            CrearPool(preloadedObjects[i].pooleable, preloadedObjects[i].amount);
        }
    }

    public void CrearPool(Pooleable pooleable, int cantidad)
    {
        if (!pool.ContainsKey(pooleable))
        {
            Queue<Pooleable> qpool = new Queue<Pooleable>();
            for (int i = 0; i < cantidad; i++)
            {
                Pooleable obj = Instantiate(pooleable);
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

  
    private void GrowPool(Pooleable pooleable, int cantidad)
    {
        for (int i = 0; i < cantidad; i++)
        {
            Pooleable obj = Instantiate(pooleable);
            obj.Desactivar();
            pool[pooleable].Enqueue(obj);
        }
    }

    public Pooleable SpawnObjeto(Pooleable obj)
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
