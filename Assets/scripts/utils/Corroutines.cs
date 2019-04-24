using System.Collections;
using System;
using UnityEngine;

namespace Utils
{
    public static class Corroutines
    {
       public static IEnumerator Wait(float time, Action start = null, Action end = null)
        {
            start?.Invoke();
            for (float i = 0; i < time; i+=Time.deltaTime)
            {
                yield return null;
            }
            end?.Invoke();
        }

        public static IEnumerator DashTransform(Transform transform,Vector3 direction, float time, float speed)
        {
            float counter = time * 30;

            for (float i = 0; i < time; i += Time.deltaTime)
            {
                transform.Mover(direction, speed);
                yield return null;
            }
        }
    }
}

