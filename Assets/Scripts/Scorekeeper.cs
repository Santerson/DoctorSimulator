using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Scorekeeper : MonoBehaviour
{
    public int Score = 0;
    [SerializeField] TextMeshProUGUI text;
    private float TextTimeGreen = 1.0f;
    private float GreenTimeLeft;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (!(GreenTimeLeft == 0))
        {
            try
            {
                if (GreenTimeLeft > 0)
                {
                    GreenTimeLeft -= Time.deltaTime;
                    text.fontSize = 26;
                    text.color = Color.green;
                }
                else
                {
                    if (text.color != Color.white)
                    {
                        text.fontSize = 22;
                        text.color = Color.white;
                    }
                }
            }
            catch
            {
                try
                {
                    //If game over while text is green
                    text.fontSize = 22;
                    text.color = Color.white;
                    GreenTimeLeft = 0;
                }
                catch { }
            }
        }
    }

    public void AddScore(int score)
    {
        this.Score += score;
        GreenTimeLeft = TextTimeGreen;
        PrintScore();
    }

    public void AddScore()
    {
        AddScore(100);
    }

    public void PrintScore()
    {
        if (text == null)
        {
            text = GameObject.Find("ScoreTMP").gameObject.GetComponent<TextMeshProUGUI>();
        }
        text.text = $"PROFIT MADE: {Score}$";
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
