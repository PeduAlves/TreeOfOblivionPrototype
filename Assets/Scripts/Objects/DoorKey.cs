using UnityEngine;

public class DoorKey : MonoBehaviour
{   
    private void Update() {
        
        transform.Rotate(0, 1, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("Key collected");
            PlayerController.Instance.hasKey = true;
            Destroy(gameObject);
        }
    }
}
