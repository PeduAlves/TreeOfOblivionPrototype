using UnityEngine;

public class CamView : MonoBehaviour
{
   private void OnTriggerEnter(Collider other) {
         if (other.CompareTag("Player")) {
            Time.timeScale = 0f; // Pausa o jogo
            PlayerController.Instance.gameOver();
         }
   }
}
