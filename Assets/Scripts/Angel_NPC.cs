using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel_NPC : MonoBehaviour
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

    private void AngelMonologue()
    {
        if (panelManager.angelPanel != null)
        {
            distance = Vector2.Distance(this.transform.position, player.transform.position);

            if (distance < dialogueActivationDistance)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    panelManager.angelPanel.SetActive(true);
                }
            }
        }
    }

    private void Update()
    {
        AngelMonologue();
    }
}
