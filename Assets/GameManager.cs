using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using  DG.Tweening;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] private GameDataManager gameData;
    [SerializeField] private PlayerUI UI;
    [SerializeField] private TransformVariable playerTransform;
    private int nivelActual;
    private Player player;

    private AvailableSkills uiOption;
    // Start is called before the first frame update

    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
            DOTween.defaultAutoPlay = AutoPlay.AutoPlayTweeners;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        player = playerTransform.value.GetComponent<Player>();
        var armas = gameData.GetArmas(gameData.GameData.armas);
        player.LoadWeapons(armas);
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
        uiOption.dash = gameData.GameData.hasDash;
        uiOption.drone = gameData.GameData.hasDrone;
        uiOption.shield = gameData.GameData.hasShield;
        UI.setUI(uiOption);
    }

    public void enableDash(bool value)
    {
        uiOption.dash = true;
        SetUI();
    }

    public void SaveData(List<ArmaData> armas)
    {
        gameData.SaveData(armas, nivelActual);
    }

    public void SaveData()
    {
        List<ArmaInventario> armas = player.armas;
        string[] armaNames = new string[armas.Count];
        for (int i = 0; i < armas.Count; i++)
        {
            armaNames[i] = armas[i].data.nombre;
        }

        GameData data = gameData.GameData;
        data.hasDash = uiOption.dash;
        data.hasDrone = uiOption.drone;
        data.hasShield = uiOption.shield;
        data.armas = armaNames;
        gameData.SaveData(data);
    }

    public void EnableDash()
    {
        uiOption.dash = true;
        UI.setUI(uiOption);
    }
}
