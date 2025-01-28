using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
     public float speed = 10.0f;
    public CharacterController controller;
    public GameObject interactiveObject;
    public Transform playerTransform;
    public float transformTime = 4f;
    public Animator animator;
    public GameObject jaguatirica, mico, Capivara, human, vult;
    private bool isTransforming = false;
    private string currentForm = "Human"; // Mantém o estado atual do jogador.
    private Vector3 moveX;
    private Vector3 moveZ;
    private float moveY;
    private float gravity = 9.8f;
    public GameObject gameOverScreen;


    public static PlayerController Instance;
    private void Awake()=>Instance = this;


    private void Update() {

        playerRotation();
        animationControl();
        playerForm();
   
    }
    private void FixedUpdate(){

        playerGravity();
        playerMove();
    }

    private void playerForm()
    {
        if (isTransforming) return;

        if (Input.GetButtonDown("CapivaraKey"))
        {
            StartCoroutine(TransformTo("Capivara", Capivara, 11));
        }
        else if (Input.GetButtonDown("MicoKey") )
        {
            StartCoroutine(TransformTo("Mico", mico, 13));
        }
        else if (Input.GetButtonDown("JaguatiricaKey"))
        {
            StartCoroutine(TransformTo("Jaguatirica", jaguatirica, 10));
        }
        else if (Input.GetButtonDown("HumanKey") )
        {
            StartCoroutine(TransformTo("Human", human, 6));
        }
        else if (Input.GetButtonDown("VultKey") )
        {
            StartCoroutine(TransformTo("Vult", vult, 12));
        }
    }

    private IEnumerator TransformTo(string newForm, GameObject newFormObject, int layer)
    {
        isTransforming = true;

        jaguatirica.SetActive(false);
        mico.SetActive(false);
        Capivara.SetActive(false);
        human.SetActive(false);
        vult.SetActive(false);

        newFormObject.SetActive(true);
        currentForm = newForm;
        gameObject.layer = layer;

        yield return new WaitForSeconds(transformTime);

        newFormObject.SetActive(false);
        isTransforming = false;
        human.SetActive(true);
        currentForm = "Human";
        gameObject.layer = 6;
    }
    private void playerGravity(){
        if(controller.isGrounded){
            moveY = 0;
        } else {
            moveY -= gravity * Time.deltaTime;
        }
    }
    private void playerMove(){
  
        if(controller.enabled){ 

        float x = Input.GetAxis("Horizontal");
        moveX = transform.right * x;
        controller.Move(moveX * speed * Time.deltaTime);
        
        float z = Input.GetAxis("Vertical");
        moveZ = transform.forward * z;
        controller.Move(moveZ * speed * Time.deltaTime);

        controller.Move(new Vector3(0f, moveY, 0f));
        }

    }
    private void playerRotation(){
        
        Vector3 direction = new Vector3(moveX.x, 0, moveZ.z);
        if(direction != Vector3.zero){
            
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, Quaternion.LookRotation(direction), 0.15f);
        }
    }
    private void animationControl(){
        if( moveX != Vector3.zero || moveZ != Vector3.zero){
            animator.SetBool("isRunning", true);
        } else {
            animator.SetBool("isRunning", false);
        }
    }
    public void gameOver(){
        gameOverScreen.SetActive(true);
    }
}
