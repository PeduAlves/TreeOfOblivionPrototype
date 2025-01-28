using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement; // Para carregar a tela de Game Over

public class Npcs : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public GameObject vision;
    public LayerMask whatIsObstacle, whatIsPlayer;

    public float visionRange = 10f; // Alcance da visão
    public float visionAngle = 45f; // Ângulo do cone de visão
    private float timeSeen = 0f; // Tempo que o jogador está na visão
    public float timeToGameOver = 3f; // Tempo necessário para o game over

    private void Update()
    {
        npcVision();
    }

    private void npcVision()
    {
        bool playerDetected = false;

        // Detectar o jogador dentro do alcance de visão
        Collider[] playerInVision = Physics.OverlapSphere(transform.position, visionRange, whatIsPlayer);
        for (int i = 0; i < playerInVision.Length; i++)
        {
            Transform target = playerInVision[i].transform;

            // Verificar se o jogador está dentro do cone de visão
            Vector3 dirToPlayer = (target.position - transform.position).normalized;
            float angleToPlayer = Vector3.Angle(transform.forward, dirToPlayer);

            if (angleToPlayer < visionAngle / 2)
            {
                // Verificar bloqueios de visão (obstáculos)
                float dstToPlayer = Vector3.Distance(transform.position, target.position);
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, whatIsObstacle))
                {
                    // Jogador detectado no cone de visão, sem obstáculos
                    transform.LookAt(target);
                    vision.SetActive(true);
                    playerDetected = true;
                    break;
                }
            }
        }

        if (playerDetected)
        {
            timeSeen += Time.deltaTime;
            if (timeSeen >= timeToGameOver)
            {
                Time.timeScale = 0f; // Pausa o jogo
                PlayerController.Instance.gameOver();
            }
        }
        else
        {
            timeSeen = 0f; // Reseta o tempo se o jogador sair da visão
            vision.SetActive(false);
        }
    }
}
