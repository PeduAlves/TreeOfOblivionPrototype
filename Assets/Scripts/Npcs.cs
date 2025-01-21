using UnityEngine;
using UnityEngine.AI;

public class Npcs : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsObstacle, whatIsPlayer;

    public float visionRange = 10f; // Alcance da visão
    public float visionAngle = 45f; // Ângulo do cone de visão

    private void Update()
    {
        npcVision();
    }

    private void npcVision()
    {
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
                    agent.SetDestination(target.position);
                    transform.LookAt(target);
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Desenhar alcance do cone de visão
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        // Obter as direções do cone de visão
        Vector3 forward = transform.forward;
        Vector3 leftBoundary = Quaternion.Euler(0, -visionAngle / 2, 0) * forward;
        Vector3 rightBoundary = Quaternion.Euler(0, visionAngle / 2, 0) * forward;

        // Desenhar limites do cone
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + leftBoundary * visionRange);
        Gizmos.DrawLine(transform.position, transform.position + rightBoundary * visionRange);

        // Desenhar raios dentro do cone
        Gizmos.color = Color.red;
        int rayCount = 30; // Número de raios dentro do cone
        for (int i = 0; i < rayCount; i++)
        {
            // Interpolar entre os limites esquerdo e direito
            float t = (float)i / (rayCount - 1);
            Vector3 direction = Vector3.Slerp(leftBoundary, rightBoundary, t).normalized;

            // Fazer o raycast para verificar bloqueios
            if (Physics.Raycast(transform.position, direction, out RaycastHit hit, visionRange, whatIsObstacle))
            {
                // Desenhar linha até o ponto onde o raio foi bloqueado
                Gizmos.DrawLine(transform.position, hit.point);
            }
            else
            {
                // Desenhar linha até o alcance máximo do cone
                Gizmos.DrawLine(transform.position, transform.position + direction * visionRange);
            }
        }
    }
}
