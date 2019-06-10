using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateNewGameButton : MonoBehaviour
{
    public TMP_InputField txmInput;

    public void CreateNewGame()
    {
        RegistrationsManager.instance.CreateNewGame(txmInput.text);
    }
}
