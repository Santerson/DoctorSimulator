using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scorekeeper : MonoBehaviour
{
    public int Score = 0;
    [SerializeField] TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        if (text == null)
        {
            Debug.LogError("No text attached!");
        }
        PrintScore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int score)
    {
        this.Score += score;
        PrintScore();
    }

    public void AddScore()
    {
        AddScore(100);
    }

    void PrintScore()
    {
        if (text == null)
        {
            text = GameObject.Find("ScoreTMP").gameObject.GetComponent<TextMeshProUGUI>();
        }
        text.text = $"Profit made: {Score}$";
    }

    public void ResetScore()
    {
        Score = 0;
        PrintScore();
    }

    public void LoseGame()
    {
        GameObject.Find("FinalScore").GetComponent<TextMeshProUGUI>().text = $"Total Profit: {Score}$";
        ResetScore();
    }
}
