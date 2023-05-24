using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InputController inputController;
    public Transform cameraTransform;
    public Transform eyes;

    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    public bool groundedPlayer;
    public float gravityValue = -9.81f;
    Vector3 initialEyesPosition;
    Vector3 posicao_agachar;
    int i = 0;
    bool agachar = false;
    public bool gancho = false;

    public Animator anim = null;
    public GameObject jogador_modelo;
    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!gancho)
        {
            if (inputController.Agachar())
            {
                agachar = !agachar;
                PlayerAgachar();
            }

            groundedPlayer = controller.isGrounded;

            

            if (groundedPlayer && playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
            }

            Vector3 camRotation = cameraTransform.eulerAngles;
            transform.rotation = Quaternion.Slerp(transform.rotation, cameraTransform.rotation, 4.5f * Time.deltaTime);

            Vector2 playerMovement = inputController.GetPlayerMoviment();
            Vector3 move = new Vector3(playerMovement.x, 0, playerMovement.y);

            //move.z = w+s; move.x = a+d; forward = para onde estamos a olhar; right = movimento dos lados
            move = transform.forward * move.z + transform.right * move.x;
            move.y = 0f;

            move.Normalize(); //para evitar movimento mais r�pido na diagonal 

            controller.Move(move * Time.deltaTime * playerSpeed);

           
                if (move.x != 0 || move.z != 0)
                {
                    float y = 0;
                    anim.SetFloat("Speed", 1.0f, 0.3f, Time.deltaTime);
                }
                else
                {
                    anim.SetFloat("Speed", 0.0f, 0.3f, Time.deltaTime);
                }
            

            if (inputController.GetPlayerJumpInThisFrame())
            {
                anim.SetTrigger("Jump");
            }

            // Changes the height position of the player..
            if (inputController.GetPlayerJumpInThisFrame() && groundedPlayer)
            {
                playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            }

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
           
        }
    }


    void PlayerAgachar()
    {
        if (agachar)
        {
            posicao_agachar = new Vector3(eyes.position.x, eyes.position.y - 1.0f, eyes.position.z);
            if (eyes.position != posicao_agachar) eyes.position = posicao_agachar;
        }
        else
        {
            initialEyesPosition = new Vector3(eyes.position.x, eyes.position.y + 1.0f, eyes.position.z);
            if (eyes.position != initialEyesPosition) eyes.position = initialEyesPosition;
        }
    }
}
