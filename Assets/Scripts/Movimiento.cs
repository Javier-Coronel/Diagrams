using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class Movimiento : MonoBehaviour
{
    private Vector3 targetPosition;
    


    private void OnTriggerEnter(Collider other) {
        other.gameObject.GetComponent<TriggerEvents>().changeScene(this.gameObject);
    }
    public void setTargetPosition(Vector3 providedTargetPosition){
        this.targetPosition = providedTargetPosition;
    }
}