using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Tooltip("The speed at which the player ACCELERATES at")]
    [SerializeField] float PlayerAccelerationSpeed = 5.0f;

    [Tooltip("The Player's max move speed")]
    [SerializeField] float PlayerMaxMoveSpeed = 3.0f;

    [Tooltip("The rate that the player slows down")]
    [SerializeField] float DampingCoefficient = 0.97f;

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
    }

    void MovePlayer()
    {
        if (RefRigidbody == null) return;

        // Creating a move vector
        Vector2 inputVector = Vector2.zero;

        // Checking for player input on the keyboard
        if (Input.GetKey(KeyCode.W)) { inputVector.y += 1; } // Up
        if (Input.GetKey(KeyCode.A)) { inputVector.x -= 1; } // Left
        if (Input.GetKey(KeyCode.S)) { inputVector.y -= 1; } // Down
        if (Input.GetKey(KeyCode.D)) { inputVector.x += 1; } // Right

        // Normalizing the vector so we can have the player move at the correct speed
        inputVector.Normalize();

        // Applying acceleration to move the player
        RefRigidbody.AddForce(inputVector * PlayerAccelerationSpeed);

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
            RefRigidbody.velocity *= DampingCoefficient;
        }

        // Storing current position
        position = transform.position;
    }
}
