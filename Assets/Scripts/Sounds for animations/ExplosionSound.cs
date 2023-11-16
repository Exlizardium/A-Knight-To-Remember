using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSound : StateMachineBehaviour
{
    private AudioManager alchemyStationAudio;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        alchemyStationAudio = FindObjectOfType<AudioManager>();
        alchemyStationAudio.Play("Explosion");
    }
}
