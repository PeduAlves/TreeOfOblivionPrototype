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

    private void AtractEnemys(){

        foreach (var enemy in enemys){

            enemy.GetComponent<NavMeshAgent>().SetDestination(localToAtract.transform.position);
        }
    }
}
