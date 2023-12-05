using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSound : StateMachineBehaviour
{
    private AudioManager walkAudio;
    private PanelManager panelManager;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        panelManager = FindObjectOfType<PanelManager>();
        walkAudio = FindObjectOfType<AudioManager>();

        walkAudio.Play("Footsteps");
        panelManager.interactButtonPrompt.SetActive(false);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        walkAudio.Stop("Footsteps");
    }
}
