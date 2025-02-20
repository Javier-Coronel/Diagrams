using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEvents : MonoBehaviour
{
    public GameObject dioramToEnable;
    public GameObject actualDioram;
    public GameObject placePosition;
    public bool esParaLatitud = true;
    public bool positiveDirection = true;
    private void Awake()
    {
        actualDioram= FindObjectsOfType<DioramaData>()[0].gameObject;
    }
    private void FixedUpdate()
    {
        FindPosition();
    }
    /**
    Metodo que cambia la escena, si la escena es un sitio situara al jugador en una posicion especificada con un GameObject.
    */
    public void changeScene(GameObject player){
        if(dioramToEnable)
        {
            dioramToEnable.SetActive(true);
            player.transform.position = placePosition.transform.position;
            player.GetComponent<Movimiento>().setTargetPosition(placePosition.transform.position);
            SceneManager.UnloadSceneAsync(gameObject.scene);
            actualDioram.SetActive(false);
            actualDioram = dioramToEnable;
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
                if (GameDataModification.Instance.dioaramas[i, j].Equals(DioramaData.Instance.dioram))
                {
                    int xMod= esParaLatitud ? 0:(positiveDirection ? 1 : -1), yMod= esParaLatitud? (positiveDirection?1:-1):0;
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
