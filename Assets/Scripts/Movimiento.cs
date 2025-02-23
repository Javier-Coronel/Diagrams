using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class Movimiento : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        other.gameObject.GetComponent<TriggerEvents>().changeDioram(this.gameObject);
    }
}