using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    private Dialogo _dialogo;
    private int currentLine;
    private int maxLine;
    private TextMeshPro tm;

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

    private void Start()
    {
        tm = GetComponent<TextMeshPro>();
    }

    public void StartDialogue(Dialogo dialogo)
    {
        dialogo.iniciado = true;
        startDialogue?.Raise();
        _dialogo = dialogo;
        currentLine = 0;
        maxLine = dialogo.dialogos.Length;
        NextLine();
        
    }

    public void NextLine()
    {
        if (currentLine==maxLine-1)
        {
            EndDialogue();
        }
        else
        {
            tm.text = _dialogo[currentLine];
            currentLine++;
        }
    }

    private void EndDialogue()
    {
        currentLine = 0;
        _dialogo.iniciado = false;
        endDialogue?.Raise();
    }
    
    
}
