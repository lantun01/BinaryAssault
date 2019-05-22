using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/ArmaData/NShot")]
public class NShot : ArmaData
{
    public int cantidadProyectiles;
    public float angulo;
    public float separacion;

    public override void Disparar(Transform arma, Vector3 direccion, float damage)
    {
        float currentAngle = 0;
        Vector3 desfase = new Vector3(0,0,0);
        Vector3 currentDesfase = new Vector3();
        float anguloDireccion = direccion.Angulo();

        for (int i = 0; i < cantidadProyectiles; i++)
        {
            currentDesfase = desfase;
            currentDesfase.RotarPunto(anguloDireccion);


            Proyectil disparo = (Proyectil)Pooler.instance.SpawnObjeto(proyectil);
            disparo.SetPatron(patron);
            disparo.transform.position = arma.position + currentDesfase;
            disparo.posInicial = arma.position + currentDesfase;
            disparo.angulo = anguloDireccion + currentAngle;
            disparo.setDamage(damage);


            if (i%2 ==0)
            {
                currentAngle -= angulo;
                desfase.y -= separacion;
            }

           // currentAngle -= i % 2 == 0 ? angulo : 0;
            currentAngle *= -1f;
            desfase *= -1;

           
        }

      
    }
}
