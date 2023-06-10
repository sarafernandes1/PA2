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
    bool agachar = false;
    public bool gancho = false;

    public Animator anim = null;
    public Animator anim_guarda = null;

    public GameObject jogador_modelo, guarda_modelo;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        anim_guarda = guarda_modelo.GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (!gancho)
        {
            Vector3 movementDirection = new Vector3(inputController.GetPlayerMoviment().x, 0, inputController.GetPlayerMoviment().y);
            Vector3 mD = movementDirection;
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

            Vector2 playerMovement = inputController.GetPlayerMoviment();
            Vector3 move = transform.right * mD.x + transform.forward * mD.z;

            controller.Move(move * playerSpeed * Time.deltaTime);

            if (move.x != 0 || move.z != 0)
            {
                float y = 0;
                anim.SetFloat("Speed", 1.0f, 0.3f, Time.deltaTime);
                 anim_guarda.SetFloat("Speed", 1.0f, 0.3f, Time.deltaTime);
            }
            else
            {
                anim.SetFloat("Speed", 0.0f, 0.3f, Time.deltaTime);
                 anim_guarda.SetFloat("Speed", 0.0f, 0.3f, Time.deltaTime);
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

            float y1 = inputController.Turning() * 150.0f * Time.deltaTime;
            transform.Rotate(Vector3.up * y1);

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
