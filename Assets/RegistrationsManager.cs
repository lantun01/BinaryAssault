using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RegistrationsManager : MonoBehaviour
{
    public static RegistrationsManager instance;
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameDataManager dataManager;
    public RegistrationPanel panelActual;
    public UnityEvent selectNewGame;
    public UnityEvent startGame;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void SetPanelActual(RegistrationPanel panel)
    {
        panelActual = panel;
        if (panelActual.isNewGame)
        {
            selectNewGame?.Invoke();
        }
        else
        {
            levelManager.LoadLevel(1);
            startGame?.Invoke();
            dataManager.SetGameData(panel.gameData, Application.persistentDataPath + "/" + panel.path);
        }
    }

    public void ActualizarPanel()
    {
        panelActual?.ReadGameData();
    }

    public void CreateNewGame(string name)
    {
        panelActual.CreateNewGame(name);
    }
    
}
