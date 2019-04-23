using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Skill : ScriptableObject {

    public abstract void Execute(Character objetivo, Action iniciar = null, Action post = null);

}
   


