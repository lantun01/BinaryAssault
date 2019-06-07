using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="ScriptableObjects/GameDataManager")]
public class GameDataManager : ScriptableObject
{
    public List<ArmaData> armas;

    public void SaveData(List<ArmaData> armas, int nivelActual, int deudaActual, string path)
    {
        string[] armasName = new string[armas.Count];
        for (int i = 0; i < armasName.Length; i++)
        {
            armasName[i] = armas[i].nombre;
        }

        GameData gameData = new GameData { armas = armasName, deuda = deudaActual, nivelActual = nivelActual };
        XMLSave.Serialize(gameData,path);
       
    }

    public GameData LoadData(string path)
    {
        return XMLSave.Deserialize<GameData>(path);
    }

    public ArmaData getArma(string nombre)
    {
        foreach (var arma in armas)
        {
            if (arma.nombre ==nombre)
            {
                return arma;
            }
        }
        return null;
    }

    public List<ArmaData> Armas(string[] nombres)
    {
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
}
