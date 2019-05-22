using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct PlayerUIOption
{
    public bool dash, upgrade, robot;
}

public class PlayerUI : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject
        botonDash,
        botonUpgrade,
        botonRobot;

    public GameObject[] toggleableUIElements;
    private int UIElementsCount;

    private void Awake()
    {
        UIElementsCount = toggleableUIElements.Length;
    }

    public void setUI(PlayerUIOption options)
    {
        bool
            dashEnable = (options.dash),
            upgradeEnable = (options.upgrade),
            robotEnable = (options.robot);

        botonDash.SetActive(dashEnable);

        botonUpgrade.SetActive(upgradeEnable);

        botonRobot.SetActive(robotEnable);
    }

    public void ShowUI(bool value)
    {
        for (int i = 0; i < UIElementsCount; i++)
        {
           toggleableUIElements[i].SetActive(value);
        }
    }
}
