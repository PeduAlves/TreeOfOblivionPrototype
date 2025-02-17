using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement; // Para carregar a tela de Game Over

public enum States
{
    PATROL, CHASE, SEARCH
}
public class Npcs : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public GameObject vision;
    public LayerMask whatIsObstacle, whatIsPlayer;
    public Transform[] waypoints;
    public int currentWaypoint = 0;
    public States state = States.PATROL;
    private bool playerDetected = false;
    public float timeForSearch = 5f; // Tempo de busca
    private Vector3 randomPoint;

    public float visionRange = 10f; // Alcance da visão
    public float visionAngle = 45f; // Ângulo do cone de visão
    private float timeSeen = 0f; // Tempo que o jogador está na visão
    public float timeToGameOver = 3f; // Tempo necessário para o game over

    private void Start() {
        
        agent.destination = waypoints[currentWaypoint].position;
        player = PlayerController.Instance.transform;
    }
    private void Update()
    {
        npcVision();
        npcAction();
    }

    private void npcVision()
    {
        playerDetected = false;

        Collider[] playerInVision = Physics.OverlapSphere(transform.position, visionRange, whatIsPlayer);
        for (int i = 0; i < playerInVision.Length; i++)
        {
            Transform target = playerInVision[i].transform;
            Vector3 dirToPlayer = (target.position - transform.position).normalized;
            float angleToPlayer = Vector3.Angle(transform.forward, dirToPlayer);

            if (angleToPlayer < visionAngle / 2)
            {
                // Verificar bloqueios de visão (obstáculos)
                float dstToPlayer = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, whatIsObstacle))
                {
                    // Jogador detectado no cone de visão, sem obstáculos
                    state = States.CHASE;
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

    private void npcAction()
    {
        switch (state)
        {
            case States.PATROL:
                npcPatrol();
                break;
            case States.CHASE:
                npcChase();
                break;
            case States.SEARCH:
                npcSearch();
                break;
        }
    }
    private void npcSearch()
    {
        StartCoroutine(searchCoroutine());
    }
    private void npcChase()
    {   
        if( playerDetected ){

            agent.destination = player.position;
        }
        else{

            state = States.SEARCH;
        }
    }
    private void npcPatrol(){

        if( !agent.pathPending && agent.remainingDistance < 0.5f){

            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            agent.destination = waypoints[currentWaypoint].position;
        }   
    }
    IEnumerator searchCoroutine(){

        yield return new WaitForSeconds(timeForSearch);
        state = States.PATROL;
    }
}
