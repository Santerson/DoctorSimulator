using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintBar : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float PlayerSprint = FindObjectOfType<PlayerMovement>().stamina;
        /*
        Gradient color = new Gradient();
        GradientColorKey[] colors = new GradientColorKey[2];
        if (PlayerSprint == 100)
        {
            colors[0] = new GradientColorKey(new Color(0,0,0,0), 0.0f);
            colors[1] = new GradientColorKey(new Color(0, 0, 0, 0), 1.0f);
        }
        else
        {
            colors[0] = new GradientColorKey(new Color(0, 0, 0, 1), 0.0f);
            colors[1] = new GradientColorKey(new Color(0, 0, 0, 1), 1.0f);
        }
        GetComponent<LineRenderer>().colorGradient = color;
        */
        GetComponent<LineRenderer>().SetPosition(0, new Vector2(FindObjectOfType<PlayerMovement>().stamina / 100 - 1, 0));
        GetComponent<LineRenderer>().SetPosition(1, new Vector2(-1, 0));
    }
}
