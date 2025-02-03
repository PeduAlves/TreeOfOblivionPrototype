using UnityEngine;

public class InstrucitionChanger : MonoBehaviour
{
   public string textToChange;

   private void OnTriggerEnter(Collider other) {
         if(other.CompareTag("Player")){
              PlayerController.Instance.instrucionText.text = textToChange;
              this.gameObject.SetActive(false);
         }
   }
}
