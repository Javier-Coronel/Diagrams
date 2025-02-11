using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerEvents : MonoBehaviour
{
    public string scene;
    public GameObject placePosition;
    /**
    Metodo que cambia la escena, si la escena es un sitio situara al jugador en una posicion especificada con un GameObject.
    */
    public void changeScene(GameObject player){
        if(!string.IsNullOrEmpty(scene)){
            SceneManager.LoadScene(scene, LoadSceneMode.Additive);
            player.transform.position = placePosition.transform.position;
            player.GetComponent<Movimiento>().setTargetPosition(placePosition.transform.position);
            SceneManager.UnloadSceneAsync(gameObject.scene);
        }
    }
}
