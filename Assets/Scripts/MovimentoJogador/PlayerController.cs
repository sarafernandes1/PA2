using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InputController inputController;
    public Transform cameraTransform;

    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    public float gravityValue = -9.81f;


    private void Start()
    {
        controller = GetComponent<CharacterController>();
      //  Cursor.visible = false;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 camRotation = cameraTransform.eulerAngles;
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, camRotation.y, transform.eulerAngles.z);

        Vector2 playerMovement = inputController.GetPlayerMoviment();
        Vector3 move = new Vector3(playerMovement.x, 0, playerMovement.y);

        //move.z = w+s; move.x = a+d; forward = para onde estamos a olhar; right = movimento dos lados
        move = transform.forward * move.z + transform.right * move.x;
        move.y = 0f;

        move.Normalize(); //para evitar movimento mais rápido na diagonal 

        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (inputController.GetPlayerJumpInThisFrame() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);


    }
}
