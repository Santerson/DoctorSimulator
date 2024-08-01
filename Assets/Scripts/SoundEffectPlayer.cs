using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    [SerializeField] AudioSource ButtonClick;
    public void PlayClickSound()
    {
        ButtonClick.Play();
    }
}
