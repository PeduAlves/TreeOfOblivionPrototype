using UnityEngine;

public class PortaoBotão : MonoBehaviour
{   
    public GameObject portao;
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

    private void Update() {
        if(PlayerController.Instance.interactiveObject == this.gameObject && Input.GetButtonDown("Interact")){
            
            if(portao.activeSelf){
                portao.SetActive(false);
            }
            else{
                portao.SetActive(true);
            }
        }
    }
}
