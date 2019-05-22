using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class BinaryTyper : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textmesh;
    private StringBuilder _stringBuilder = new StringBuilder(16);

    [SerializeField]private String title = "BINARY ASSAULT";
    [SerializeField] private float characterTime = 0.05f;
    private WaitForSeconds waitTime;

    private String opciones = "10";
    // Start is called before the first frame update
    void Start()
    {
        waitTime = new WaitForSeconds(characterTime);
        StartCoroutine(Show());
    }

    IEnumerator Show()
    {
        int index = 0;
        for (int i = 0; i < title.Length+1; i++)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append(title);
            for (int k = 0; k < 3; k++)
            {
                for (int j = i; j < title.Length; j++)
                {
                    int opcion = UnityEngine.Random.Range(0, 2);   
                    _stringBuilder.Insert(j, opciones[opcion]);
                }
            
                textmesh.text = _stringBuilder.ToString(0,title.Length);
                yield return waitTime;
            }

           
        }
    }
}
