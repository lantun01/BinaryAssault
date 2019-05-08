using UnityEngine;
using UnityEngine.UI;
using Utils;

public class BarraUI : MonoBehaviour
{
    public Image barra;
    public Image barraSombra;
    public FloatVariable ratio;

   public void SetFillRatio(float ratio)
    {
        StopAllCoroutines();
        if (ratio>barra.fillAmount)
        {
            barraSombra.color = Color.green;
            barraSombra.fillAmount = ratio;
            StartCoroutine(Animations.FadeBar(barra, ratio, 0.5f));
        }
        else
        {
            barraSombra.color = Color.yellow;
            barra.fillAmount = ratio;
            StartCoroutine(Animations.FadeBar(barraSombra, ratio, 0.5f));
        }
    }

    public void ActualizarBarra()
    {

        SetFillRatio(ratio.value);
    }
}
