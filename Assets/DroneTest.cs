using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class DroneTest : MonoBehaviour, IUpdateable
{
   public Transform target;

   public float speed;
   private bool orbitando;
   private float angle;
   [SerializeField] private float sqrRadio = 3f;
   private float radio;
   private Vector3 targetPosition;
   private Player master;
   [SerializeField] private Arma _arma;
    public bool enCombate;
    [SerializeField] private float lifeSpan = 5;
    [SerializeField] private float timeCounter;
    private bool deployed;
   [SerializeField] private Animator _animator;
   private int morirTriggerHash;



#if UNITY_EDITOR

   private void OnValidate()
   {
      _animator = GetComponent<Animator>();
   }

#endif
   public void Inicializar(Player p)
   {
      master = p;
   }
   
   private void Start()
   {
      morirTriggerHash = Animator.StringToHash("morir");
     radio = Mathf.Sqrt(sqrRadio);
   }
   private void SimpleFollow()
   {
      targetPosition = target.position;
      Vector3 dir; 

      if (orbitando)
      {
         angle += Time.deltaTime*speed;
         
         float offsetX =  Mathf.Cos(angle) * radio;
         float offsetY = Mathf.Sin(angle) * radio;
         Vector3 newPos = new Vector3(offsetX+targetPosition.x,offsetY+targetPosition.y);
         transform.position = newPos;
         if (enCombate)
         {
            dir = targetPosition - transform.position;
            _arma.Disparar(dir);
         }
      }
      
      else
      {
         dir = targetPosition - transform.position;
         float sqrDist = Vector3.SqrMagnitude(dir);
         transform.position += dir.normalized * Time.deltaTime * speed;
         
         if (sqrDist<sqrRadio)
         {
            orbitando = true;
            dir = transform.position - targetPosition;
            angle = dir.Angulo()*Mathf.Deg2Rad;
            
         }
      }

   }

   public void Deploy()
   {
      if (deployed) return;

      enCombate = false;
      deployed = true;
      orbitando = false;
      timeCounter = 0;
      transform.position = master.transform.position+Vector3.right;
      
      if (master.objetivo)
      {
         target = master.objetivo.transform;
         enCombate = true;
      }
      else
      {
         target = master.transform;
      }
      
      gameObject.SetActive(true);
      Subscribir();
   }

   public void Reset()
   {
      orbitando = false;
      target = master.transform;
      enCombate = false;
   }

   public void CustomUpdate()
   {
      timeCounter += Time.deltaTime;
      if (timeCounter > lifeSpan)
      {
        Displace();
      }
      else
      {
         SimpleFollow();
      }   
      
   }

   public void Displace()
   {
      deployed = false;
      UpdateManager.instance.RetirarElemento(this);
      _animator.SetTrigger(morirTriggerHash);
   }

   public void SetTarget(Transform target)
   {
      this.target = target;
      orbitando = false;
   }

   public void Subscribir()
   {
      UpdateManager.instance.Subscribe(this);
   }

   
   //Este método es llamado desde la animación morir
   public void Deshabilitar()
   {
      gameObject.SetActive(false);
   }
}
