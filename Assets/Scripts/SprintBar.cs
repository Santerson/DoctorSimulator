using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprintBar : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        float PlayerSprint = FindObjectOfType<PlayerMovement>().stamina;
        
        GetComponent<LineRenderer>().SetPosition(0, new Vector2(FindObjectOfType<PlayerMovement>().stamina / 100 - 1, 0));
        GetComponent<LineRenderer>().SetPosition(1, new Vector2(-1, 0));

        if (PlayerSprint >= 100)
        {
            Gradient grad = new Gradient();
            var colors = new GradientColorKey[2];
            colors[0] = new GradientColorKey(new Color(0.54f, 1, 0.52f), 0.0f);
            var alphas = new GradientAlphaKey[1];
            alphas[0] = new GradientAlphaKey(1.0f, 0.0f);
            grad.SetKeys(colors, alphas);
            GetComponent<LineRenderer>().colorGradient = grad;
        }
        else
        {
            Gradient grad = new Gradient();
            var colors = new GradientColorKey[2];
            colors[0] = new GradientColorKey(new Color(0, 0.8f, .08320045f), 0.0f);
            colors[1] = new GradientColorKey(new Color(0, .409434f, 0.05100772f), 0.0f);
            var alphas = new GradientAlphaKey[1];
            alphas[0] = new GradientAlphaKey(1.0f, 0.0f);
            grad.SetKeys(colors, alphas);
            GetComponent<LineRenderer>().colorGradient = grad;
        }
    }
}
