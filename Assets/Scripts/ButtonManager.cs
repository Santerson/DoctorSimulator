using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

    
   public void GoToMainScene()
    {
        SceneManager.LoadScene("Main");
    }
    public void GoToCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    public void GoToStart()
    {
        SceneManager.LoadScene("Start");
    }
    public void GoToStartFromEnd()
    {
        FindObjectOfType<FinalScore>().deleteText();
        SceneManager.LoadScene("Start");
    }
    public void GoToTips()
    {
        SceneManager.LoadScene("");
    }
}
