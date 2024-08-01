using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    [SerializeField] AudioSource ButtonClick;
    [SerializeField] AudioSource Bells;
    [SerializeField] AudioSource Grabbing;
    [SerializeField] AudioSource TakingMed;
    [SerializeField] AudioSource RightMed;
    [SerializeField] AudioSource WrongMed;
    [SerializeField] AudioSource Tick;

    [SerializeField] AudioSource MenuMusic;
    [SerializeField] AudioSource GameplayMusic;

    public void PlayClickSound()
    {
        ButtonClick.Play();
    }
    public void PlayBells()
    {
        Bells.Play();
    }
    public void PlayGrabbing()
    {
        Grabbing.Play();
    }
    public void PlayTakingMed()
    {
        TakingMed.Play();
    }
    public void PlayRightMed()
    {
        RightMed.Play();
    }
    public void PlayWrongMed()
    {
        WrongMed.Play();
    }
    public void PlayTick()
    {
        Tick.Play();
    }

    //LOOPING TRACKS
    public void StartMenuMusic() 
    {
        MenuMusic.Play();
    }
    public void StopMenuMusic()
    {
        MenuMusic.Stop();
    }

    public void PlayGameplayMusic()
    {
        GameplayMusic.Play();
    }
    public void StopGameplayMusic()
    {
        GameplayMusic.Stop();
    }
}
