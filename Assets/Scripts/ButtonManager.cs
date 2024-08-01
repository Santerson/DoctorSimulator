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
            FindObjectOfType<SoundEffectPlayer>().StopMenuMusic();
            FindObjectOfType<SoundEffectPlayer>().PlayGameplayMusic();
        }
        catch { Debug.LogError("Play from start scene for audio"); }
        SceneManager.LoadScene("Main");
    }
    public void GoToCredits()
    {
        try
        {
            FindObjectOfType<SoundEffectPlayer>().PlayClickSound();
        }
        catch { Debug.LogError("Play from start scene for audio"); }
        SceneManager.LoadScene("Credits");
    }
    public void GoToStart()
    {
        try
        {
            FindObjectOfType<SoundEffectPlayer>().PlayClickSound();
        }
        catch { Debug.LogError("Play from start scene for audio"); }
        SceneManager.LoadScene("Start");
    }
    public void GoToStartFromEnd()
    {
        try
        {
            FindObjectOfType<SoundEffectPlayer>().PlayClickSound();
        }
        catch { Debug.LogError("Play from start scene for audio"); }
        FindObjectOfType<FinalScore>().deleteText();
        SceneManager.LoadScene("Start");
    }
    public void GoToTips()
    {
        try
        {
            FindObjectOfType<SoundEffectPlayer>().PlayClickSound();
        }
        catch { Debug.LogError("Play from start scene for audio"); }
        SceneManager.LoadScene("Tips");
    }
    public void QuitToDesktop()
    {
        try
        {
            FindObjectOfType<SoundEffectPlayer>().PlayClickSound();
        }
        catch { Debug.LogError("Play from start scene for audio"); }
        Application.Quit();
    }
}
