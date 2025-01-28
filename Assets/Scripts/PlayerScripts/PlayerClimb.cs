using UnityEngine;

public class PlayerClimb : MonoBehaviour
{
    public Transform climbStartPoint; // Ponto inicial da escalada
    public Transform climbEndPoint;   // Ponto final da escalada
    public float climbSpeed = 5f;     // Velocidade de escalada
    private bool isClimbing = false;
    private CharacterController controller; // Controlador do jogador

    
    public static PlayerClimb Instance;
    private void Awake()=>Instance = this;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {   
        if (isClimbing)
        {
            // Move o jogador do ponto inicial ao ponto final
            float step = climbSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, climbEndPoint.position, step);

            // Verifica se chegou ao ponto final
            if (Vector3.Distance(transform.position, climbEndPoint.position) < 0.1f)
            {
                EndClimb();
            }
        }
    }

    public void StartClimb(Transform climbArea)
    {
        // Configura os pontos de escalada com base no objeto de escalada
        climbStartPoint = climbArea.Find("ClimbStart");
        climbEndPoint = climbArea.Find("ClimbEnd");

        if (climbStartPoint != null && climbEndPoint != null)
        {
            isClimbing = true;
            controller.enabled = false; // Desativa o controlador do jogador durante a escalada
        }
    }

    private void EndClimb()
    {
        isClimbing = false;
        controller.enabled = true; // Reativa o controlador
    }
}
