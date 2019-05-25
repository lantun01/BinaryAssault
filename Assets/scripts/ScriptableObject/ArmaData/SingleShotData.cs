using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/ArmaData/SingleShot")]
public class SingleShotData : ArmaData
{
    public override void Disparar(Transform arma, Vector3 direccion, float damage)
    {
        Proyectil disparo = (Proyectil)Pooler.instance.SpawnObjeto(proyectil);
        disparo.SetPatron(patron);
        disparo.transform.position = arma.position;
        disparo.posInicial = arma.position;
        disparo.angulo = direccion.Angulo();
        disparo.setDamage(damage);
        disparo.Disparar(damage,direccion);
    }
}
