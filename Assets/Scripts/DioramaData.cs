using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[ExecuteAlways]
public class DioramaData : MonoBehaviour
{
    public static DioramaData Instance;
    public string dioram="";
    public bool updateToSceneName=true;
    void Awake()
    {
        if(Application.isPlaying)
        {
            Instance = this;
        }
    }
    void Start()
    {
        dioram = gameObject.scene.name;
# if UNITY_EDITOR
        FindObjectsOfType<TMP_Text>()[0].text = dioram;
#endif
    }
}
