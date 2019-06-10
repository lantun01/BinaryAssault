using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIPanel : MonoBehaviour
{

    public Vector3 initialPosition;
    private RectTransform rectTransform;
    [SerializeField] private float toggleSpeed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

   public void Show()
    {
        rectTransform.anchoredPosition = initialPosition;
        rectTransform.DOScaleY(1, toggleSpeed).From(0);
    }

    public void Hide()
    {
        rectTransform.DOScaleY(0, toggleSpeed);
    }
}
