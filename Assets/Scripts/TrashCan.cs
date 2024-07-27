using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    [SerializeField] Vector2 DetectionBottomLeft;
    [SerializeField] Vector2 DetectionTopRight;
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] ParticleSystem TrashParticles;

    // Start is called before the first frame update
    void Start()
    {
        text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
    }

    private void OnDrawGizmosSelected()
    {
        DebugExtensions.DrawBox(DetectionBottomLeft, DetectionTopRight, Color.red);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPos = FindObjectOfType<PlayerMovement>().transform.position;
        if (FindObjectOfType<Pickup>().Holding && SUtilities.IsInRange(playerPos, DetectionBottomLeft, DetectionTopRight)) FadeIn();
        else { FadeOut(); }
        if (SUtilities.IsInRange(playerPos, DetectionBottomLeft, DetectionTopRight))
        {
            if (FindObjectOfType<Pickup>().Holding && Input.GetKey(KeyCode.Space))
            {
                TrashParticles.Play();
                FindObjectOfType<Pickup>().Holding = false;
                FindObjectOfType<TakeDepositOrders>().ObjectHolding.GetComponent<PickupableObjects>().DropByPlayer();
            }
        }
    }

    void FadeOut()
    {
        if (text.color.a <= 0)
        {
            return;
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a - 0.01f);

    }

    void FadeIn()
    {
        if (text.color.a >= 1)
        {
            return;
        }
        text.color = new Color(text.color.r, text.color.g, text.color.b, text.color.a + 0.01f);

    }
}