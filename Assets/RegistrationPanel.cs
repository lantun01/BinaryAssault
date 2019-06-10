using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RegistrationPanel : MonoBehaviour
{

    [SerializeField] private GameDataManager dataManager;
    public string path;
    public GameData gameData { get; set; }
    [SerializeField] private TextMeshProUGUI debtCounter;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private GameObject deleteButton;
    [SerializeField] private GameObject droneIcon;
    [SerializeField] private GameObject shieldIcon;
    [SerializeField] private GameObject dashIcon;
    public bool isNewGame { get; set; } = true;

    void Start()
    {
        ReadGameData();
    }

    public void ReadGameData()
    {
        print(Application.persistentDataPath);
        gameData = dataManager.LoadData(Application.persistentDataPath + "/" + path);
        if (gameData != null)
        {
            deleteButton.SetActive(true);
            isNewGame = false;
            debtCounter.text = gameData.deuda.ToString();
            nameText.text = gameData.userName;
            if (gameData.hasDash)
            {
                dashIcon.SetActive(true);
            }
            else
            {
                dashIcon.SetActive(false);
            }
            if (gameData.hasDrone)
            {
                droneIcon.SetActive(true);
            }
            else
            {
                droneIcon.SetActive(false);
            }

            if (gameData.hasShield)
            {
                shieldIcon.SetActive(true);
            }
            else
            {
                shieldIcon.SetActive(false);
            }
        }
        else
        {
            deleteButton.SetActive(false);
            isNewGame = true;
            debtCounter.text = "0";
            nameText.text = "New Game";
            shieldIcon.SetActive(false);
            droneIcon.SetActive(false);
            dashIcon.SetActive(false);
        }
    }

    public void SelectPanel()
    {
        RegistrationsManager.instance.SetPanelActual(this);
    }

    public void CreateNewGame(string name)
    {
        gameData = new GameData();
        gameData.userName = name;
        dataManager.SaveData(userName:name, path:Application.persistentDataPath+"/"+path);
        ReadGameData();
    }

    public void Delete()
    {
        dataManager.DeleteData(Application.persistentDataPath + "/" + path);
        ReadGameData();
    }

}
