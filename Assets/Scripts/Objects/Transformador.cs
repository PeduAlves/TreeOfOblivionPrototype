using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformador : MonoBehaviour
{
    public List<GameObject> cams;
     private void Update() {
        if(PlayerController.Instance.interactiveObject == this.gameObject && Input.GetButtonDown("Interact")){

            foreach( GameObject cam in cams){
                cam.SetActive(false);
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
