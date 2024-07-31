using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PickupableObjects : MonoBehaviour
{
    [SerializeField] Vector2 DetectionBottomLeft;
    [SerializeField] Vector2 DetectionTopRight;
    Vector2 Position;
    [SerializeField] TextMeshProUGUI text;
    GameObject Player;
    public bool BeingHeld = false;

    private void Awake()
    {
        Player = FindObjectOfType<PlayerMovement>().gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
        Position = transform.position;
    }

    private void OnDrawGizmosSelected()
    {
        DebugExtensions.DrawBox(DetectionBottomLeft, DetectionTopRight, Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPos = FindObjectOfType<PlayerMovement>().transform.position;
        if (!FindObjectOfType<Pickup>().Holding && SUtilities.IsInRange(playerPos, DetectionBottomLeft, DetectionTopRight)) FadeIn();
        else { FadeOut(); }
        if (SUtilities.IsInRange(playerPos, DetectionBottomLeft, DetectionTopRight))
        {
            if (!FindObjectOfType<Pickup>().Holding && Input.GetKey(KeyCode.Space))
            {
                FindObjectOfType<Pickup>().Holding = true;
                BeingHeld = true;
                text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
                FindObjectOfType<TakeDepositOrders>().ObjectHolding = gameObject;
            }
        }
        if (BeingHeld)
        {
            CarriedByPlayer();
        }
        else
        {
            GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
    }

    private void CarriedByPlayer()
    {
        GetComponent<SpriteRenderer>().sortingOrder = 2;
        transform.position = new Vector2(Player.transform.position.x - 0.05f, Player.transform.position.y - 0.3f);
    }

    /// <summary>
    /// Script that will destroy the object the player is holding and return it
    /// </summary>
    public void DropByPlayer()
    {
        if (BeingHeld == false) 
        {
            Debug.LogError("Player is not holding anything: Returning");
            return; 
        }
        BeingHeld = false;
        FindObjectOfType<Pickup>().Holding = false;
        transform.position = Position;
    }

    void FadeOut()
    {
        if (text.color.a <= 0)
        {
            return;
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - (Time.deltaTime * 3f));

    }

    void FadeIn()
    {
        if (text.color.a >= 1)
        {
            return;
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + (Time.deltaTime * 3f));

    }
}
