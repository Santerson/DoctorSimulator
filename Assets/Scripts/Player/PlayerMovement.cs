using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 3;
    public float rotationSpeed;
    public float sprintSpeed = 5;
    public float stamina = 100;
    public float staminaDrain = 1f;
    public bool isSprinting;
    private float horizontalInput;
    private float forwardInput;
    [SerializeField] private Rigidbody2D playerRb;
    void Update()
    {
        // Movement
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
        playerRb.velocity = new Vector2(horizontalInput * speed, forwardInput * speed);
        transform.up = playerRb.velocity.normalized * rotationSpeed * Time.deltaTime;

        // Sprinting
        if (Input.GetKeyDown(KeyCode.LeftShift) && stamina > 0)
        {
            speed = sprintSpeed;
            isSprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift) || stamina <= 0)
        {
            speed = 3;
            isSprinting = false;
        }

        // Stamina Management
        if (isSprinting)
        {
            stamina -= staminaDrain * Time.deltaTime;
        }
        else
        {
            stamina += 10 * Time.deltaTime;
        }
        stamina = Mathf.Clamp(stamina, 0, 100);

    }
}
