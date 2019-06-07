using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataTest : MonoBehaviour
{
    public GameDataManager gameDataManager;
    public List<ArmaData> armas;
    public GameData gameData;
    public string path;

    private void Start()
    {
        //SaveData();
        LoadData();
    }

    public void SaveData()
    {
        gameDataManager.SaveData(armas, 5, 100,path);
    }

    public void LoadData()
    {
        gameData = gameDataManager.LoadData(path);
        armas = gameDataManager.Armas(gameData.armas);
    }
}
