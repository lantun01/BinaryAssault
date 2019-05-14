using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class DialogManager : MonoBehaviour
{
    private Dialogo _dialogo;
    private int currentLine;
    private int maxLine;
   [FormerlySerializedAs("TextBox")] [SerializeField] private TextMeshProUGUI textMesh;
   [SerializeField] private GameObject textBox;

    public static DialogManager instance;
    public GameEvent endDialogue;
    public GameEvent startDialogue;
    public RectTransform dialogueBox;
    private Camera camera;
    
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
        camera = Camera.main;
    }


    public void StartDialogue(Dialogo dialogo, Vector3 position)
    {
        textBox.gameObject.SetActive(true);
        dialogo.iniciado = true;
        startDialogue?.Raise();
        _dialogo = dialogo;
        currentLine = 0;
        maxLine = dialogo.dialogos.Length;
        Vector2 screenPos = camera.WorldToScreenPoint(position);
        dialogueBox.position = screenPos;
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
            textMesh.text = _dialogo[currentLine];
            currentLine++;
        }
    }

    private void EndDialogue()
    {
        currentLine = 0;
        _dialogo.iniciado = false;
        endDialogue?.Raise();
        textBox.gameObject.SetActive(false);
        
    }
    
   
    
    
}
