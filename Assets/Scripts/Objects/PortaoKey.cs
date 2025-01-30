using UnityEngine;

public class PortaoKey : MonoBehaviour
{   
    public GameObject portao;
    private void Update() {
        if(PlayerController.Instance.interactiveObject == this.gameObject && Input.GetButtonDown("Interact")
        && PlayerController.Instance.hasKey){
            
            if(portao.activeSelf){
                portao.SetActive(false);
            }
            else{
                portao.SetActive(true);
            }
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

