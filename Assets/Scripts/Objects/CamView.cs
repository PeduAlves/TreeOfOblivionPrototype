using UnityEngine;

public class CamView : MonoBehaviour
{
   private void OnTriggerEnter(Collider other) {
         if (other.CompareTag("Player")) {
              Debug.Log("Player detected!");
         }
   }
}
