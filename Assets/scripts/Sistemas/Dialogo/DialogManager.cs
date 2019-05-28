using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    private Dialogo _dialogo;
    private int currentLine;
    private int maxLine;
    [SerializeField] private TextMeshProUGUI textMesh;
   [SerializeField] private GameObject textBox;

    public static DialogManager instance;
    public GameEvent endDialogue;
    public GameEvent startDialogue;
    public Action endDialogAction;
    public RectTransform dialogueBox;
    private Camera camera;
    private  WaitForSeconds _waitForSeconds = new WaitForSeconds(0.02f);
    private AudioSource _audioSource;
    private StringBuilder _stringBuilder = new StringBuilder(256);

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
        _audioSource = GetComponent<AudioSource>();
    }


    public void StartDialogue(Dialogo dialogo, Vector3 position)
    {
        print("iniciadooo");
        StopAllCoroutines();
        textBox.gameObject.SetActive(true);
        dialogo.iniciado = true;
        startDialogue?.Raise();
        _dialogo = dialogo;
        currentLine = 0;
        maxLine = dialogo.dialogos.Length;
        Vector2 screenPos = camera.WorldToScreenPoint(position);
        dialogueBox.position = screenPos;
        NextDialog();
        
    }

    public void NextDialog()
    {
        if (currentLine==maxLine)
        {
            EndDialogue();
        }
        else
        {
            _stringBuilder.Clear();
          StartCoroutine(DialogAnimation(_dialogo[currentLine]));
           // textMesh.text = _dialogo[currentLine];
            currentLine++;
        }
    }

    private void EndDialogue()
    {
        currentLine = 0;
        _dialogo.iniciado = false;
        textBox.gameObject.SetActive(false);
        endDialogue?.Raise();
        endDialogAction?.Invoke();
    }

    private IEnumerator DialogAnimation(String dialog)
    {
        int length = dialog.Length;
        textMesh.text = "";
        _stringBuilder.Append(dialog);

        
        for (int i = 0; i < length; i++)
        {
            textMesh.text = _stringBuilder.ToString(0,i+1);
            _audioSource.Play();
        yield return _waitForSeconds;
        }
    }
    
}
