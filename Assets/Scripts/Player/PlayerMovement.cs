using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("The speed at which the player ACCELERATES at")]
    [SerializeField] float PlayerAccelerationSpeed = 5.0f;

    [Tooltip("The Player's max move speed")]
    [SerializeField] float PlayerMaxMoveSpeed = 3.0f;

    [Tooltip("The rate that the player slows down")]
    [SerializeField] float DampingCoefficient = 0.97f;

    [SerializeField] float sprintSpeed = 4.5f;

    [SerializeField] TextMeshProUGUI SprintText;

    public float stamina = 100;
    public float staminaDrain = 15f;
    public bool isSprinting;
    public float staminaCooldown = 3f;
    private float staminaDepletedTime = 0f;

    Rigidbody2D RefRigidbody = null;
    Animator animator;

    Vector2 position = Vector2.zero;

    private void Awake()
    {
        RefRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        if (RefRigidbody == null)
        {
            Debug.LogError("Player must have a Rigidbody2D component");
        }
    }

    void Update()
    {
        MovePlayer();
        float speed = RefRigidbody.velocity.magnitude;
        //Debug.Log(stamina);
        //Debug.Log(PlayerMaxMoveSpeed);
    }

    void MovePlayer()
    {
        if (RefRigidbody == null) return;

        // Creating a move vector
        Vector2 inputVector = Vector2.zero;

        // Checking for player input on the keyboard
        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y += 1;
            animator.SetFloat("BackwardSpeed", Mathf.Abs(inputVector.y));
        } // Up
        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x -= 1;
            animator.SetFloat("LeftSpeed", Mathf.Abs(inputVector.x));
        } // Left
        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y -= 1;
            animator.SetFloat("forwardSpeed", Mathf.Abs(inputVector.y));
        } // Down
        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1;
            animator.SetFloat("RightSpeed", Mathf.Abs(inputVector.x));
        } // Right

        if (inputVector == Vector2.zero)
        {
            animator.SetFloat("forwardSpeed", 0);
            animator.SetFloat("LeftSpeed", 0);
            animator.SetFloat("RightSpeed", 0);
            animator.SetFloat("BackwardSpeed", 0);
        }

        //Sprint function
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (stamina == 0)
            {
                isSprinting = false;
            }
            else
            {
                isSprinting = true;
            }
        }
        else
        {
            isSprinting = false;
        }
        // Stamina Management
        if (isSprinting)
        {
            PlayerMaxMoveSpeed = sprintSpeed;
            PlayerAccelerationSpeed = sprintSpeed;
            stamina -= staminaDrain * Time.deltaTime;
        }
        else
        {
            PlayerMaxMoveSpeed = 3.0f;
            PlayerAccelerationSpeed = 5.0f;
            if (stamina == 0)
            {
                staminaDepletedTime += Time.deltaTime;
                if (staminaDepletedTime >= staminaCooldown)
                {
                    stamina += 10 * Time.deltaTime;
                }
            }
            else
            {
                stamina += 10 * Time.deltaTime;
                staminaDepletedTime = 0;
            }
        }
        stamina = Mathf.Clamp(stamina, 0, 100);

        // Normalizing the vector so we can have the player move at the correct speed
        inputVector.Normalize();

        // Applying acceleration to move the player
        RefRigidbody.AddForce(inputVector * PlayerAccelerationSpeed * (Time.deltaTime * 1000));

        // Checking if the player is moving faster than their maximum movement speed
        if (RefRigidbody.velocity.magnitude > PlayerMaxMoveSpeed)
        {
            // Limiting the speed to PlayerMaxMoveSpeed
            RefRigidbody.velocity = RefRigidbody.velocity.normalized * PlayerMaxMoveSpeed;
        }

        // Damping the movement when no input is given
        if (inputVector.sqrMagnitude <= 0.1f)
        {
            // Applying damping effect to slow down the player
            RefRigidbody.velocity *= DampingCoefficient * Time.deltaTime;
        }

        // Storing current position
        position = transform.position;

        if (stamina != 100)
        {
            Color textColor = Color.green;
            if (stamina <= 0.3)
            {
                textColor = Color.black;
            }
            else if (stamina <= 25)
            {
                textColor = Color.red;
            }
            else if (stamina <= 50)
            {
                textColor = Color.yellow;
            }
            SprintText.color = textColor;
            SprintText.text = $"Stamina: {stamina:#0.0}";
        }
        else 
        {
            SprintText.text = "";
        }
    }
}
