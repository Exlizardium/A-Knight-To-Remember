using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mute_button : MonoBehaviour
{
    public bool muted = false;

    public void MuteOrUnmute()
    {
        muted = !muted;
        AudioListener.pause = muted;
    }
}
