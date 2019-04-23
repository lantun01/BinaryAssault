using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/Skills/Dash")]
public class DashSkill : Skill
{
    public float velocidad;
    public override void Execute(Character objetivo, Action iniciar = null, Action terminar = null)
    {
        iniciar?.Invoke();
        objetivo.rb.Mover(objetivo.mirada, velocidad);
        terminar?.Invoke();

    }
}
