using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using  DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private PlayerUI UI;

    [SerializeField] private PlayerUIOption uiOption;
    // Start is called before the first frame update

    private void Awake()
    {
        
        if (instance == null)
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
        Application.targetFrameRate = -1;
        QualitySettings.vSyncCount = 0;
        SetUI();


    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetUI()
    {
        UI.setUI(uiOption);
    }

    public void enableDash(bool value)
    {
        uiOption.dash = true;
        SetUI();
    }
}
