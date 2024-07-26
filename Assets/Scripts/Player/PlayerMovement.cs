using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float defaultSpeed = 10.0f;
    public float sprintSpeed = 30.0f;
    public float acceleration = 8.0f;
    public float deceleration = 12.0f;
    public bool isSprinting;
    public float stamina = 100;
    public float staminaDrain = 1.0f;

    private Rigidbody2D rb;
    private float activeSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        activeSpeed = defaultSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        MovementInput();
    }

    void MovementInput()
    {
        // Read input and store as direction vector
        Vector2 moveInput;
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();

        // Determine speed based on shift key
        if (Input.GetKey(KeyCode.LeftShift))
        {
            activeSpeed = sprintSpeed;
            isSprinting = true;
        }
        else
        {
            activeSpeed = defaultSpeed;
            isSprinting = false;
        }
        if (isSprinting)
        {
            stamina -= staminaDrain * Time.deltaTime;
        }
        else
        {
            stamina += 10 * Time.deltaTime;
        }
        stamina = Mathf.Clamp(stamina, 0, 100);

        // Fetch current velocity
        Vector2 currentVelocity = rb.velocity;

        // If there is any movement input
        if (moveInput != Vector2.zero)
        {
            currentVelocity = moveInput * currentVelocity.magnitude;
            // If velocity not yeat reached speed, accelerate
            if (currentVelocity.magnitude < activeSpeed)
            {
                currentVelocity += acceleration * Time.deltaTime * moveInput;
            }
            // Ensure velocity doesn't go over speed
            else
            {
                currentVelocity = activeSpeed * moveInput;
            }
        }
        // Checking if there is no input
        else
        {
            // If there is still movement, we need to decelerate
            if (currentVelocity.magnitude > 0)
            {
                currentVelocity -= deceleration * Time.deltaTime * currentVelocity.normalized;
            }
            // Ensure velocity doesn't go below zero
            else
            {
                currentVelocity = Vector2.zero;
            }
        }

        // Update rigidbody velocity
        rb.velocity = currentVelocity;
    }
}
