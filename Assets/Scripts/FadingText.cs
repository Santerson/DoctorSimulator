using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FadingText : MonoBehaviour
{
    [Tooltip("How long will the text take before appearing")]
    [SerializeField] float PreDuration = 0f;
    float Pre;
    [Tooltip("How long the text will be visible before disapearing")]
    [SerializeField] float Duration = 5f;
    [SerializeField] string Message = "Replace with your text! (check script settings)";
    float Left;
    TextMeshProUGUI text = null;
    // Start is called before the first frame update
    void Start()
    {
        Left = Duration;
        Pre = PreDuration;
        text = GetComponent<TextMeshProUGUI>();
        if (text == null)
        {
            Debug.LogError("No TMPro attached to object!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (text != null)
        {
            if (Pre > 0)
            {
                text.text = "";
                Pre -= Time.deltaTime;
            }
            else
            {
                text.text = Message;
                Left -= Time.deltaTime;
                if (Left <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
