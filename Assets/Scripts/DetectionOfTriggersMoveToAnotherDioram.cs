using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class DetectionOfTriggersMoveToAnotherDioram : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        other.gameObject.GetComponent<TriggerEvents>().changeDioram(gameObject);
    }
}