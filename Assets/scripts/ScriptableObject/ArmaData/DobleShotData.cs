using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/ArmaData/DobleShot")]

public class DobleShotData : ArmaData
{
    public PatronDisparo patron2;
    public override void Disparar(Transform arma, Vector3 direccion, float damage)
    {
        Proyectil shot1 = (Proyectil)Pooler.instance.SpawnObjeto(proyectil);
        Proyectil shot2 = (Proyectil)Pooler.instance.SpawnObjeto(proyectil);
        shot1.setDamage(damage);
        shot1.SetPatron(patron);
        shot1.transform.position = arma.position;
        shot1.posInicial = arma.position;
        shot1.angulo = direccion.Angulo();

        shot2.SetPatron(patron2);
        shot2.transform.position = arma.position;
        shot2.posInicial = arma.position;
        shot2.angulo = direccion.Angulo();
        shot2.setDamage(damage);
    }
}
