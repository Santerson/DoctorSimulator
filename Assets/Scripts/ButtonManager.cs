using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{

   public void GoToMainScene()
    {
        try
        {
            FindObjectOfType<SoundEffectPlayer>().PlayClickSound();
        }
        catch { }
        SceneManager.LoadScene("Main");
    }
    public void GoToCredits()
    {
        try
        {
            FindObjectOfType<SoundEffectPlayer>().PlayClickSound();
        }
        catch { }
        SceneManager.LoadScene("Credits");
    }
    public void GoToStart()
    {
        try
        {
            FindObjectOfType<SoundEffectPlayer>().PlayClickSound();
        }
        catch { }
        SceneManager.LoadScene("Start");
    }
    public void GoToStartFromEnd()
    {
        try
        {
            FindObjectOfType<SoundEffectPlayer>().PlayClickSound();
        }
        catch { }
        FindObjectOfType<FinalScore>().deleteText();
        SceneManager.LoadScene("Start");
    }
    public void GoToTips()
    {
        try
        {
            FindObjectOfType<SoundEffectPlayer>().PlayClickSound();
        }
        catch { }
        SceneManager.LoadScene("Tips");
    }
    public void QuitToDesktop()
    {
        try
        {
            FindObjectOfType<SoundEffectPlayer>().PlayClickSound();
        }
        catch { }
        Application.Quit();
    }
}
