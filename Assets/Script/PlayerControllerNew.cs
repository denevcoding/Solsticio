using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerNew : MonoBehaviour
{
    //Variables de movimiento
    private float horizontalMove;
    private float verticalMove;
    private bool runMove;


    public bool isGrounded;
    public bool isJumping;

    private Vector3 playerInput;
    
    public CharacterController player;
    public float playerSpeed;
    public float gravity;
    public float fallVelocity;
    public float jumpForce;

    //Variables de movimiento relativo a camara
   
    public Camera mainCamera;
    private Vector3 camForward;
    private Vector3 camRight;
    private Vector3 movePlayer;

    [Header("Speed properties")]
    public float walkSpeed;
    public float runSpeed;
    

    //Variables animacion

    public Animator playerAnimatorController;
    void Start()
    {
        player = GetComponent<CharacterController>();
        playerAnimatorController = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       
        isGrounded = player.isGrounded;
        playerAnimatorController.SetBool("IsGrounded",isGrounded);

        if (isGrounded)
        {
            isJumping = false;
            playerAnimatorController.SetBool("isJumping", isJumping);
        }

        PlayerInput();        
        //Movimiento camara
        camDirection();


        SetGravity();
        player.Move(movePlayer * Time.deltaTime);
    }


    void PlayerInput()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");
        runMove = Input.GetKey("left shift");

        playerInput = new Vector3(horizontalMove, 0, verticalMove);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        playerAnimatorController.SetFloat("PlayerWalkVelocity", playerInput.magnitude * playerSpeed);


        WalkRun();
        PlayerSkill();
    }

    void camDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;



        movePlayer = playerInput.x * camRight + playerInput.z * camForward;
        movePlayer = movePlayer * playerSpeed;

        if (playerInput.magnitude > 0.1f)
        {
            float turnSpeed = 5f;
            Quaternion dirQ = Quaternion.LookRotation(movePlayer);
            Quaternion slerp = Quaternion.Slerp(transform.rotation, dirQ, turnSpeed * Time.deltaTime);
            transform.localRotation = slerp;
        }
       

        //player.transform.LookAt(player.transform.position + movePlayer);


    }

    //Funciones para la Habilidades

    public void PlayerSkill()
    {
        //Jump
        if (player.isGrounded && Input.GetButtonDown("Jump"))
        {

            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;

            isJumping = true;
            playerAnimatorController.SetBool("isJumping", isJumping);
            //playerAnimatorController.SetTrigger("PlayerJump");
        }
    }
    

 
    

    public void WalkRun()
    {
        if (playerInput == Vector3.zero)
            return;

        if (Input.GetKey(KeyCode.LeftShift))        
            SetSpeed(runSpeed);        
        else        
            SetSpeed(walkSpeed);
        
        ////Run
        //if (playerInput != Vector3.zero && Input.GetKey(KeyCode.LeftShift))
        //{
        //    SetSpeed(runSpeed);
        //}
        //else if (playerInput != Vector3.zero && !Input.GetKey(KeyCode.LeftShift))
        //{
        //    SetSpeed(walkSpeed);
        //}
    }


    public void SetSpeed(float speed)
    {
        playerSpeed = speed;
    }
   





    public void SetGravity()
    {
        fallVelocity -= gravity * Time.deltaTime;
        movePlayer.y = fallVelocity;
        //if (player.isGrounded)
        //{
        //    fallVelocity = -gravity * Time.deltaTime;
        //    movePlayer.y = fallVelocity;
        //}
        //else
        //{
           
            
        //}
        

        playerAnimatorController.SetFloat("PlayerVerticalVelocity", player.velocity.y);
    }



    private void OnAnimatorMove()
    {

    }
}
