using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class DioramaData : MonoBehaviour
{
    public string dioram="";
    public bool updateToSceneName=true;
    void Awake()
    {
        if(!Application.isPlaying&&updateToSceneName){
            dioram=gameObject.scene.name;
            GameObject.FindObjectsOfType<TMP_Text>()[0].text=dioram;
        }
    }
}
