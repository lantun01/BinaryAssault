using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(menuName ="ScriptableOjects/Variables/float")]
public class FloatVariable : ScriptableObject
{
    public Action<float> ValueChanged;
    public float value;

    public void Raise()
    {
        ValueChanged?.Invoke(value);
    }
}
