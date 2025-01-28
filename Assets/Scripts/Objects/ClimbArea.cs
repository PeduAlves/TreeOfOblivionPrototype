using UnityEngine;

public class ClimbArea : MonoBehaviour
{
   private void Update() {
    if(PlayerController.Instance.interactiveObject == this.gameObject && Input.GetButtonDown("Interact")){
            
            PlayerClimb.Instance.StartClimb(this.transform);
            
        }
   }
    private void OnTriggerEnter(Collider other) {
        
        if(other.CompareTag("Player")){
            print("Pode Interagir");
            PlayerController.Instance.interactiveObject = this.gameObject;
        }
    }
    
    private void OnTriggerExit(Collider other) {
        if(other.CompareTag("Player")){
            print("Não pode Interagir");
            PlayerController.Instance.interactiveObject = null;
        }
    }
}
