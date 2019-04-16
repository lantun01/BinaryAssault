using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ArmaData/TripleShot")]

public class TripleShot : ArmaData
{
    public Proyectil proyectil2;
    public Proyectil proyectil3;
    public PatronDisparo patron2;
    public PatronDisparo patron3;
    public float angulo;
    public float separacion;

    public override void Disparar(Transform arma, Vector3 direccion)
    {
        Vector3 desfase = new Vector3(0, separacion,0);

        Proyectil disparo = (Proyectil)Pooler.instance.SpawnObjeto(proyectil);
        disparo.SetPatron(patron);
        disparo.transform.position = arma.position;
        disparo.posInicial = arma.position;
        disparo.angulo = direccion.Angulo();

        Proyectil p2 = (Proyectil)Pooler.instance.SpawnObjeto(proyectil2);
        p2.SetPatron(patron2);
        p2.transform.position = arma.position + desfase;
        p2.posInicial = arma.position+desfase.RotarPunto(direccion.Angulo());
        p2.angulo = direccion.Angulo() + angulo;

        p2 = (Proyectil)Pooler.instance.SpawnObjeto(proyectil3);
        p2.SetPatron(patron3);
        p2.transform.position = arma.position - desfase;
        p2.posInicial = arma.position-desfase;
        p2.angulo = direccion.Angulo() - angulo;

    }

   
}
