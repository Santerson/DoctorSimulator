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
    public void GoToTips()
    {
        SceneManager.LoadScene("Tips");
    }
    public void GoToStart()
    {
        SceneManager.LoadScene("Start");
    }
}
