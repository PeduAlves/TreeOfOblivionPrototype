using System.Collections.Generic;
using UnityEngine;

public class SecurityCam : MonoBehaviour
{
    public Transform player; // Referência ao jogador
    public float detectionRange = 10f; // Alcance da visão
    public float rotationSpeed = 2f; // Velocidade de rotação da câmera
    public float leftLimit = -45f; // Limite esquerdo de rotação
    public float rightLimit = 45f; // Limite direito de rotação
    private bool movingRight = true; // Indica a direção atual da rotação
  

    private void Update()
    {   
        //UpdateVisionMesh();
        PatrolArea();
    }

    private void PatrolArea()
    {
        // Realiza a interpolação da rotação
        float targetAngle = movingRight ? rightLimit : leftLimit;
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Alterna a direção ao atingir os limites
        if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
        {
            movingRight = !movingRight;
        }
    }

}
