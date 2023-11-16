using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_For_Buttons : MonoBehaviour
{
    private AudioManager buttonAudio;

    private void Start()
    {
        buttonAudio = FindObjectOfType<AudioManager>();
    }

    public void ButtonSound()
    {
        buttonAudio.Play("Button");
    }
}
