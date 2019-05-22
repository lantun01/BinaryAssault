using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
   public UnityEvent startLoading;
   public UnityEvent endLoading;


   public void LoadLevel(int sceneIndex)
   {
      startLoading?.Invoke();
      StartCoroutine(LoadAsynchronously(sceneIndex));
   }

   IEnumerator LoadAsynchronously(int sceneIndex)
   {
      AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);
      while (!operation.isDone)
      {
         yield return null;
      }
      endLoading?.Invoke();
   }
}
