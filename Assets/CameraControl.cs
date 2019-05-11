using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
   public CinemachineTargetGroup TargetGroup;


   private CinemachineTargetGroup.Target target;
   [SerializeField] private float targetWeight;
   
   public void SetTarget(MonoBehaviour target)
   {
      
      this.target.target = target.transform;
      this.target.weight = targetWeight;
      TargetGroup.m_Targets[1] = this.target;
   }

   public void Reset()
   {
      target.target = null;
      target.weight = 0;
      TargetGroup.m_Targets[1] = target;
   }
}
