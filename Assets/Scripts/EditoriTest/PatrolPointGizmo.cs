using UnityEngine;

public class PatrolPointGizmo : MonoBehaviour
{
  private void OnDrawGizmos() {
    Gizmos.color = Color.green;
    Gizmos.DrawSphere(transform.position, 0.5f);
  }
}
