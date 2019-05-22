using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  DG.Tweening;

public class CinematicBars : MonoBehaviour
{
   [SerializeField] RectTransform topBar, bottomBar;
   private Rect topbarRect, bottomBarRect;
   [SerializeField] Vector2 delta = new Vector2(0,100);
   private  Vector2 zero = Vector2.zero;
   [SerializeField] private float duration;

   private void Start()
   {
      topbarRect = topBar.rect;
      bottomBarRect = bottomBar.rect;
   }

   public void DeployBars()
   {
      DOTween.To(() => topBar.sizeDelta, x => topBar.sizeDelta = x, delta, duration);
      DOTween.To(() => bottomBar.sizeDelta, x => bottomBar.sizeDelta = x, delta, duration);
   }

   public void RemoveBars()
   {
      DOTween.To(() => bottomBar.sizeDelta, x => bottomBar.sizeDelta = x, zero,duration);
      DOTween.To(() => topBar.sizeDelta, x => topBar.sizeDelta = x, zero,duration);
   }
}
