using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloseTo : MonoBehaviour
{
    [SerializeField] Vector2 BottomLeftCornerOfPickupArea = new Vector2(3, 3);
    [SerializeField] Vector2 TopRightCornerOfPickupArea = new Vector2(-3, -3);
    [SerializeField] GameObject AppearingItem = null;
    [SerializeField] GameObject Orders;
    // Start is called before the first frame update
    void Start()
    {
        if (AppearingItem == null)
        {
            Debug.LogError("No appearingitem found! (Check serialize fields)");
        }
    }

    private void OnDrawGizmosSelected()
    {
        DebugExtensions.DrawBox(BottomLeftCornerOfPickupArea, TopRightCornerOfPickupArea, Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        
        Vector2 playerPos = FindObjectOfType<PlayerMovement>().transform.position;
        //Debug.Log(playerPos);
        if (playerPos.x > BottomLeftCornerOfPickupArea.x &&
            playerPos.x < TopRightCornerOfPickupArea.x &&
            playerPos.y < BottomLeftCornerOfPickupArea.y &&
            playerPos.y > TopRightCornerOfPickupArea.y &&
            !Orders.GetComponent<TakeDepositOrders>().OrderTaken)
        {
            //Debug.Log("Appearing");
            AppearingItem.SetActive(true);
        }
        else
        {
            //Debug.Log("Disappearing");
            AppearingItem.SetActive(false);
        }

        if (playerPos.x > BottomLeftCornerOfPickupArea.x &&
            playerPos.x < TopRightCornerOfPickupArea.x &&
            playerPos.y < BottomLeftCornerOfPickupArea.y &&
            playerPos.y > TopRightCornerOfPickupArea.y &&
            FindObjectOfType<Pickup>().Holding)
        {
            PickupableObjects[] Objs = FindObjectsOfType<PickupableObjects>();
            for (int i = 0; i < Objs.Length; i++)
            {
                PickupableObjects curr = Objs[i];
                if (curr.BeingHeld)
                {
                    Debug.Log($"This is a {curr.name} object");
                    if (curr.name == "PickupRed")
                    {
                        FindObjectOfType<TakeDepositOrders>().CheckGivenMedicine(0);
                    }
                    if (curr.name == "PickupGreen")
                    {
                        FindObjectOfType<TakeDepositOrders>().CheckGivenMedicine(1);
                    }
                    if (curr.name == "PickupBlue")
                    {
                        FindObjectOfType<TakeDepositOrders>().CheckGivenMedicine(2);
                    }
                }
            }
        }
    }
}
