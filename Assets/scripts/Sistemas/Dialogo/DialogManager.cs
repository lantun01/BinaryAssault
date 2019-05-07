using System;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    private Dialogo _dialogo;
    private int currentLine;
    private int maxLine;
   [SerializeField] private TextMeshPro TextBox;

    public static DialogManager instance;
    public GameEvent endDialogue;
    public GameEvent startDialogue;
    
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



    public void StartDialogue(Dialogo dialogo, Vector3 position)
    {
        TextBox.gameObject.SetActive(true);
        TextBox.transform.position = position;
        dialogo.iniciado = true;
        startDialogue?.Raise();
        _dialogo = dialogo;
        currentLine = 0;
        maxLine = dialogo.dialogos.Length;
        NextLine();
        
    }

    public void NextLine()
    {
        if (currentLine==maxLine)
        {
            EndDialogue();
        }
        else
        {
            TextBox.text = _dialogo[currentLine];
            currentLine++;
        }
    }

    private void EndDialogue()
    {
        currentLine = 0;
        _dialogo.iniciado = false;
        endDialogue?.Raise();
        TextBox.gameObject.SetActive(false);
        
    }
    
   
    
    
}
