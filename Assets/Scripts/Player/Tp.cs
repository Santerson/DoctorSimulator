using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tp : MonoBehaviour
{
    // Name of the scene to switch to

    // This function is called when the collider attached to this object enters a collision with another collider
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the collision is tagged as "Player"
        if (other.CompareTag("Player"))
        {
            // Load the specified scene
            SceneManager.LoadScene("MedicineFabrication");
        }
    }
}

