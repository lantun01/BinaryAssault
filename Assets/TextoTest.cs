using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TextoTest : MonoBehaviour
{
    private TextMeshProUGUI tm;
    private TextMeshPro wea;
    // Start is called before the first frame update
    void Start()
    {
        tm = GetComponent<TextMeshProUGUI>();
        wea = GetComponent<TextMeshPro>();
        DOTween.Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
