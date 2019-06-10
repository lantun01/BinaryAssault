using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaver : MonoBehaviour
{
    public GameDataManager gameDataManager;
    public List<ArmaData> armas;
    public GameData gameData;
    private string userName;

    public void SaveData()
    {
        gameDataManager.SaveData(armas, 5, 100, gameDataManager.path, userName);
    }
}
