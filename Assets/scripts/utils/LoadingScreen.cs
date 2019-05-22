using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{
   private int clipHash;
   private Material _material;
   [SerializeField] private Image Image;

   private void Start()
   {
      _material = Image.material;
      clipHash = Shader.PropertyToID("_ClippingRight_Value_1");
   }

   public void StartLoading()
   {
      _material.DOFloat(1, clipHash, 0.5f);
   }

   public void EndLoading()
   {
      _material.DOFloat(0, clipHash, 0.5f);
   }
}
