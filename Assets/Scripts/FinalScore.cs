using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinalScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Text;
    // Start is called before the first frame update
    void Start()
    {
        Text.text = "";
    }

    public void GameLose()
    {
        Text.text = $"Score + {FindObjectOfType<Scorekeeper>().Score}";
    }

    public void deleteText()
    {
        Text.text = "";
    }
}
