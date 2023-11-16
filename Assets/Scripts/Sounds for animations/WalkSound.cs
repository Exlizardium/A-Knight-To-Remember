using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : StateMachineBehaviour
{
    private AudioManager walkAudio;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        walkAudio = FindObjectOfType<AudioManager>();
        walkAudio.Play("Footsteps");
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        walkAudio.Stop("Footsteps");
    }

}
