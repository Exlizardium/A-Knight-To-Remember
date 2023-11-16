using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    Player_Movement player;
    SecondRoomSpawner levelManager;
    [SerializeField] private Animator portalAnimator;
    [SerializeField] private float numberOfKeysRequired = 4;
    private AudioManager portalAudio;
    private PanelManager panelManager;

    private bool portalOpen = false;
    private bool playerIsWithinOpenPortal = false;
    private float levelNumberBeforeBoss = 3;

    private void Start()
    {
        player = FindObjectOfType<Player_Movement>();
        levelManager = FindObjectOfType<SecondRoomSpawner>();
        portalAudio = FindObjectOfType<AudioManager>();
        panelManager = FindObjectOfType<PanelManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (portalOpen == true)
        {
            portalAnimator.SetBool("portalActivated", true);
        }
        else
        {
            portalAnimator.SetBool("portalOpenedCheckFromScript", true);
            if (portalAudio != null)
            {
                portalAudio.Play("Portal closed");
            }
            Invoke("ResetClosedPortalTrigger", 1f);
        }
       
    }

    private void OnCollisionExit(Collision collision)
    {
        playerIsWithinOpenPortal = false;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(portalOpen == true)
        {
            portalAnimator.SetTrigger("teleporting");
            playerIsWithinOpenPortal = true;
        } 
    }

    private void Teleport()
    {
        if(playerIsWithinOpenPortal == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (levelManager.levelCount == levelNumberBeforeBoss)
                {
                    player.numberOfKeysObtained = 0;
                    portalAudio.Stop("Main theme");
                    portalAudio.Play("Boss theme");
                    levelManager.GenerateBossLevel();
                }
                else if(levelManager.levelCount == levelNumberBeforeBoss + 1)
                {
                    panelManager.victoryScreen.SetActive(true);
                    portalAudio.Stop("Boss theme");
                    portalAudio.Play("Victory theme");
                }
                else
                {
                    player.numberOfKeysObtained = 0;
                    levelManager.GenerateNewLevel();
                }
            }     
        }
    }

    private void ResetClosedPortalTrigger()
    {
        portalAnimator.SetBool("portalOpenedCheckFromScript", false);
    }

    private void CheckIfPortalCanOpen()
    {
        if (player.numberOfKeysObtained == numberOfKeysRequired)
        {
            portalOpen = true;
            this.gameObject.tag = "Interactive";
        }
    }

    private void Update()
    {
        CheckIfPortalCanOpen();
        Teleport();
    }
}
