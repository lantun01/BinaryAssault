﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour {

    public static UpdateManager instance;

    private void Awake()
    {
        instance = this;
    }

    private List<IUpdateable> updateables = new List<IUpdateable>();
    int elementos;

	
    public void Subscribe(IUpdateable elemento)
    {
        updateables.Add(elemento);
        elementos++;
    }

    public void RetirarElemento(IUpdateable elemento)
    {
        updateables.Remove(elemento);
        elementos--;
    }

	// Update is called once per frame
	void Update () {
        for (int i = 0; i < elementos; i++)
        {
            updateables[i].CustomUpdate();
        }
	}
}