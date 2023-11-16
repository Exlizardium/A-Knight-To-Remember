using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monk_NPC : MonoBehaviour
{
    [SerializeField] private float dialogueActivationDistance = 1;
    PanelManager panelManager;
    private Player_Movement player;
    private float distance;

    private void Start()
    {
        player = FindObjectOfType<Player_Movement>();
        panelManager = FindObjectOfType<PanelManager>();
    }

    private void MonkMonologue()
    {
        if (panelManager.monkPanel != null)
        {
            distance = Vector2.Distance(this.transform.position, player.transform.position);

            if (distance < dialogueActivationDistance)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    panelManager.monkPanel.SetActive(true);
                }
            }
        }
    }

    private void Update()
    {
        MonkMonologue();
    }
}
