using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfSprintBarr : MonoBehaviour
{
    LineRenderer RefLineRenderer = null;

    private void Start()
    {
        RefLineRenderer = GetComponent<LineRenderer>();
    }
    private void Update()
    {
        float sprint = FindObjectOfType<PlayerMovement>().stamina;
        if (sprint <= 0)
        {
            Gradient grad = new Gradient();
            var colors = new GradientColorKey[1];
            colors[0] = new GradientColorKey(new Color(0.5f, 0, 0), 0.0f);
            var alphas = new GradientAlphaKey[1];
            alphas[0] = new GradientAlphaKey(1.0f, 0.0f);
            grad.SetKeys(colors, alphas);
            RefLineRenderer.colorGradient = grad;
        }
        else
        {
            Gradient grad = new Gradient();
            var colors = new GradientColorKey[1];
            colors[0] = new GradientColorKey(new Color(0.4433962f, 0.4433962f, 0.4433962f), 0.0f);
            var alphas = new GradientAlphaKey[1];
            alphas[0] = new GradientAlphaKey(1.0f, 0.0f);
            grad.SetKeys(colors, alphas);
            RefLineRenderer.colorGradient = grad;
        }
    }
}
