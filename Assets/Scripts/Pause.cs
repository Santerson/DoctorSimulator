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
        if (!FindObjectOfType<TakeDepositOrders>().GameStarted) return;
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!GamePaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        if (!FindObjectOfType<TakeDepositOrders>().GameStarted) return;
        GamePaused = true;
        transform.position = Vector2.zero;
    }

    public void PauseButton()
    {
        if (!FindObjectOfType<TakeDepositOrders>().GameStarted) return;
        try
        {
            FindObjectOfType<SoundEffectPlayer>().PlayClickSound();
        }
        catch { Debug.LogError("Play from the start for sound effects!"); }
        PauseGame();
    }

    public void ResumeGame()
    {
        if (!FindObjectOfType<TakeDepositOrders>().GameStarted) return;
        GamePaused = false;
        transform.position = Position;
    }

    public void ResumeButton()
    {
        if (!FindObjectOfType<TakeDepositOrders>().GameStarted) return;
        try
        {
            FindObjectOfType<SoundEffectPlayer>().PlayClickSound();
        }
        catch { Debug.LogError("Play from the start for sound effects!"); }
        ResumeGame();
    }
}
