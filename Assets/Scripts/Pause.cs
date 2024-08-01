using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool GamePaused = false;
    Vector2 Position;
    // Start is called before the first frame update
    void Start()
    {
        Position = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GamePaused)
            {
                GamePaused = true;
                PauseGame();
            }
            else
            {
                GamePaused = false;
                ResumeGame();
            }
        }
    }


    private void PauseGame()
    {
        transform.position = Vector2.zero;
    }
    private void ResumeGame()
    {
        transform.position = Position;
    }
}
