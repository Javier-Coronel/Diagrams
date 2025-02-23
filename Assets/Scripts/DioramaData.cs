using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DioramaData : MonoBehaviour
{
    public string dioram="";
    public TMP_Text debugText;
    void Start()
    {
        dioram = gameObject.name;
        debugText.text = dioram;
    }
}
