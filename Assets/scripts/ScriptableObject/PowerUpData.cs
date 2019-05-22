using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/PowerUp")]
public class PowerUpData : ScriptableObject
{
   public TipoPowerUp tipo;
   public float cantidad;
   public float duracion;
   public Sprite sprite;


}

public  enum  TipoPowerUp {ataque,salud,velocidad,invulnerabilidad}
