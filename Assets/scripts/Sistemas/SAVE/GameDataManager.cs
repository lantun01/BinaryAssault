using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


[CreateAssetMenu(menuName = "ScriptableObjects/GameDataManager")]
public class GameDataManager : ScriptableObject
{
    public List<ArmaData> armas;
    public GameData GameData;
    public string path;

    public void SaveData(List<ArmaData> armas = null, int nivelActual = 0, int deudaActual = 0, string path = "", string userName = "", AvailableSkills skills = default)
    {
        string[] armasName = null;
        if (armas != null)
        {
            armasName = new string[armas.Count];
            for (int i = 0; i < armasName.Length; i++)
            {
                armasName[i] = armas[i].nombre;
            }

        }

        GameData gameData = new GameData
        {
            armas = armasName,
            deuda = deudaActual,
            nivelActual = nivelActual,
            userName = userName,
            hasDash = skills.dash,
            hasDrone = skills.drone,
            hasShield = skills.shield
        };

        XMLSave.Serialize(gameData, path);
    }

    public void SaveData(GameData data)
    {
        XMLSave.Serialize(data, path);
    }


    public GameData LoadData(string path)
    {
        try
        {
            return XMLSave.Deserialize<GameData>(path);
        }
        catch
        {
            return null;
        }
    }

    public ArmaData getArma(string nombre)
    {
        foreach (var arma in armas)
        {
            if (arma.nombre == nombre)
            {
                return arma;
            }
        }
        return null;
    }

    public List<ArmaData> GetArmas(string[] nombres)
    {
        if (nombres ==null)
        {
            return null;
        }
        var listArmas = new List<ArmaData>();
        foreach (var nombre in nombres)
        {
            foreach (var arma in armas)
            {
                if (arma.nombre == nombre)
                {
                    listArmas.Add(arma);
                }
            }
        }
        return listArmas;
    }

    public void DeleteData(string path)
    {
        FileInfo file = new FileInfo(path);
        file.Delete();
    }

    public void SetGameData(GameData gameData, string path)
    {
        this.GameData = gameData;
        this.path = path;
    }
}
