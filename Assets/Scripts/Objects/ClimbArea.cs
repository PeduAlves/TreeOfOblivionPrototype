using UnityEngine;

public class ClimbArea : MonoBehaviour
{
   private void Update() {
    if(PlayerController.Instance.interactiveObject == this.gameObject && Input.GetButtonDown("Interact") 
    && PlayerController.Instance.currentForm == "Mico"){
            
            PlayerClimb.Instance.StartClimb(this.transform);
            
        }
   }
    private void OnTriggerEnter(Collider other) {
        
        if(other.CompareTag("Player")){
            PlayerController.Instance.interactObject.SetActive(true);
            PlayerController.Instance.interactiveObject = this.gameObject;
        }
    }
    
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            PlayerController.Instance.interactObject.SetActive(false);
            PlayerController.Instance.interactiveObject = null;
        }
    }
}
