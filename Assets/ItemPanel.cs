using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemPanel : MonoBehaviour
{

  public static ItemPanel instance;
  
  [SerializeField] private TextMeshProUGUI textMeshName;
  [SerializeField] private TextMeshProUGUI textMeshDescription;
  [SerializeField] private Image itemImage;
  [SerializeField] private float hidePosition;
  [SerializeField] private RectTransform rect;

  private Sequence ShowAndHide;


  private void Awake()
  {
    if (instance ==null)
    {
      instance = this;
    }
    else
    {
      Destroy(this);
    }
  }

  private void Start()
  {
    ShowAndHide = DOTween.Sequence();
    rect = GetComponent<RectTransform>();
    hidePosition = -rect.sizeDelta.y;
    
    ShowAndHide.SetAutoKill(false);
    ShowAndHide.Append(rect.DOMoveY(20, 0.5f))
      .AppendInterval(2.5f)
      .Append(rect.DOMoveY(hidePosition, 0.5f));
    ShowAndHide.Pause();

  }

  public void Show(string itemName, string itemDescription, Sprite itemSprite)
  {
    textMeshName.text = itemName;
    textMeshDescription.text = itemDescription;
    itemImage.sprite = itemSprite;
    
    ShowAndHide.Restart();
  }

  public void Show(ArmaData data)
  {
    print("Hi!");
    Show(data.nombre, data.descripcion, data.sprite);
  }
}
