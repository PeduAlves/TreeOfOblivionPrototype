using UnityEngine;

public class VultChargeRestore : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerController.Instance.vultCharges = 3;
            foreach(GameObject charge in PlayerController.Instance.vultChargesIcon)
            {
                charge.SetActive(true);
            }
        };
    }
}
