using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerEvents : MonoBehaviour
{
    public GameObject dioramToEnable;
    public GameObject actualDioram;
    public GameObject placePosition;
    public bool esParaLatitud = true;
    public bool positiveDirection = true;
    private void FixedUpdate()
    {
        actualDioram= FindObjectsOfType<DioramaData>(false)[0].gameObject;
        FindPosition();
    }
    public void changeDioram(GameObject player){
        if(dioramToEnable)
        {
            dioramToEnable.SetActive(true);
            player.transform.position = placePosition.transform.position;
            actualDioram.SetActive(false);
        }
        else
        {
            Debug.Log("");
        }
    }
    void FindPosition()
    {
        for (int i = 0; i < GameDataModification.Instance.dioaramas.GetLength(0); i++){
            for (int j = 0; j < GameDataModification.Instance.dioaramas.GetLength(1); j++)
            {
                if (GameDataModification.Instance.dioaramas[i, j].Equals(actualDioram))
                {
                    int xMod = 0,yMod=0;
                    if(esParaLatitud){
                        yMod = positiveDirection ? 1 : -1;
                    }else{
                        xMod = positiveDirection ? 1 : -1;
                    }
                    
                     
                    Debug.Log(transform.name+" "+xMod+" "+yMod);
                    if (0 <= i + xMod && 0 <= j + yMod && GameDataModification.Instance.dioaramas.GetLength(0) > i + xMod && GameDataModification.Instance.dioaramas.GetLength(1) > j + yMod)
                    {
                        GetComponent<Collider>().isTrigger = true;
                        dioramToEnable = GameDataModification.Instance.dioaramas[i + xMod, j + yMod];
                    }
                    else {
                        dioramToEnable = null;
                        GetComponent<Collider>().isTrigger = false;
                    }
                    return;
                }
            }
        }
    }
}
