using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


namespace Utils
{
    public static class Animations 
    {
       public static IEnumerator FadeBar(Image image, float ratio, float time)
        {
            float dif = ratio-image.fillAmount;
            float amount = dif / (60*time);
            float repeat = time * 60;
            for (int i = 0; i < repeat; i++)
            {
                image.fillAmount += amount;
                yield return null;
            }
            image.fillAmount = ratio;

        }
    }
}


