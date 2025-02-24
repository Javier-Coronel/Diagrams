using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TriggerEvents : MonoBehaviour
{
    public GameObject dioramToEnable;
    public GameObject placePosition;
    public bool esParaLatitud = true;
    public bool positiveDirection = true;
    private void FixedUpdate()
    {
        FindPosition();
    }
    public void changeDioram(GameObject player){
        if(dioramToEnable)
        {
            dioramToEnable.SetActive(true);
            player.transform.position = placePosition.transform.position;
            GameDataModification.Instance.actualDioram.SetActive(false);
            GameDataModification.Instance.actualDioram = dioramToEnable;
        }
        else
        {
            Debug.Log("");
        }
    }
    void FindPosition()
    {
        Vector2Int actualPos = GameDataModification.Instance.FindPositionOfActualDioram();
        if(actualPos.x>-1){
            int xMod = 0,yMod=0;
            if(esParaLatitud){
                yMod = positiveDirection ? 1 : -1;
            }else{
                xMod = positiveDirection ? 1 : -1;
            }
            Debug.Log(transform.name+" "+xMod+" "+yMod);
            if (0 <= actualPos.x + xMod && 0 <= actualPos.y + yMod && GameDataModification.Instance.dioaramas.GetLength(0) > actualPos.x + xMod && GameDataModification.Instance.dioaramas.GetLength(1) > actualPos.y + yMod)
            {
                GetComponent<Collider>().isTrigger = true;
                dioramToEnable = GameDataModification.Instance.dioaramas[actualPos.x + xMod, actualPos.y + yMod];
            }
            else {
                dioramToEnable = null;
                GetComponent<Collider>().isTrigger = false;
            }
        }
    }
}
