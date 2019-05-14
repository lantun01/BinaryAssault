using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using  DG.Tweening;

public class CameraControl : MonoBehaviour
{
   public CinemachineTargetGroup TargetGroup;
   [SerializeField] private CinemachineVirtualCamera vc;
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

   public void ZoomIn()
   {
      DOTween.To(() => vc.m_Lens.OrthographicSize, x => vc.m_Lens.OrthographicSize = x,2f ,0.3f);
   }

   public void ZoomOut()
   {
      DOTween.To(() => vc.m_Lens.OrthographicSize, x => vc.m_Lens.OrthographicSize= x, 2.73f, 0.3f);
   }
}
