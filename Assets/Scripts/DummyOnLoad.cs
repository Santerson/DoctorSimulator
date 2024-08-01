using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        FindObjectOfType<Scorekeeper>().PrintScore();
    }
}
