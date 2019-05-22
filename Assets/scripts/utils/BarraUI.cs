using System;
using UnityEngine;
using UnityEngine.UI;
using Utils;
using  DG.Tweening;

public class BarraUI : MonoBehaviour
{
    public RectTransform barra;
    public Image barraSombra;
    public FloatVariable ratio;
    [SerializeField] private Color textFullColor, textEmpyColor;
    [SerializeField] private Color backgroundFullColor, backgroundEmpyColor;
    [SerializeField] private Gradient textGradient;
    [SerializeField] private Gradient backgroundGradient;

    private float maxWidth;
    private Vector2 sizeDelta;
    [SerializeField] private Image barraImg;
    [SerializeField] private Material healthMaterial;
    private int materialColorIndex;
    
    

    private void Start()
    {
        healthMaterial = barraImg.material;
        maxWidth = barra.rect.width;
        sizeDelta = barra.sizeDelta;
        
        ActualizarBarra();
        

    }

    public void SetFillRatio(float ratio)
    {
        float currentFill = barra.sizeDelta.x / maxWidth;
        float width = ratio * maxWidth;
        float currentWidth = barra.sizeDelta.x;
        StopAllCoroutines();
      //  Color nextColor = Color.Lerp(textEmpyColor, textFullColor, ratio);
      Color nextColor = textGradient.Evaluate(ratio);
        barraSombra.DOColor(Color.Lerp(backgroundEmpyColor, backgroundFullColor, ratio), 0.5f);
        healthMaterial.DOColor(nextColor,"_TintRGBA_Color_1", 0.5f);
        if (ratio>currentFill)
        {
            barraSombra.fillAmount = ratio;
            DOTween.To(() => barra.sizeDelta.x,x => { sizeDelta.x = x;
                barra.sizeDelta = sizeDelta;
            }, width ,0.5f);
        }
        else
        {
            barraSombra.fillAmount = ratio;
            DOTween.To(() => barra.sizeDelta.x,x => { sizeDelta.x = x;
                barra.sizeDelta = sizeDelta;
            }, width ,0.5f);
        }
    }

    public void ActualizarBarra()
    {

        SetFillRatio(ratio.value);
    }
}
