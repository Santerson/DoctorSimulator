using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCloseTo : MonoBehaviour
{
    [SerializeField] Vector2 BottomLeftCornerOfPickupArea = new Vector2(3, 3);
    [SerializeField] Vector2 TopRightCornerOfPickupArea = new Vector2(-3, -3);
    [SerializeField] GameObject AppearingItem = null;
    // Start is called before the first frame update
    void Start()
    {
        if (AppearingItem == null)
        {
            Debug.LogError("No appearingitem found! (Check serialize fields)");
        }
    }

    // Update is called once per frame
    void Update()
    {
        DebugExtensions.DrawBox(BottomLeftCornerOfPickupArea, TopRightCornerOfPickupArea, Color.red);
        Vector2 playerPos = FindObjectOfType<PlayerMovement>().transform.position;
        //Debug.Log(playerPos);
        if (playerPos.x > BottomLeftCornerOfPickupArea.x &&
            playerPos.x < TopRightCornerOfPickupArea.x &&
            playerPos.y < BottomLeftCornerOfPickupArea.y &&
            playerPos.y > TopRightCornerOfPickupArea.y)
        {
            //Debug.Log("Appearing");
            AppearingItem.SetActive(true);
        }
        else
        {
            //Debug.Log("Disappearing");
            AppearingItem.SetActive(false);
        }
    }
}
