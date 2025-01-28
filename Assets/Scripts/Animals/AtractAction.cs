using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AtractAction : MonoBehaviour
{
    public List<GameObject> enemys;
    public GameObject localToAtract;

    private void Update() {
        
        if(PlayerController.Instance.interactiveObject == this.gameObject && Input.GetButtonDown("Interact")){
            
            AtractEnemys();
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

    private void AtractEnemys(){

        foreach (var enemy in enemys){

            enemy.GetComponent<NavMeshAgent>().SetDestination(localToAtract.transform.position);
        }
    }
}
