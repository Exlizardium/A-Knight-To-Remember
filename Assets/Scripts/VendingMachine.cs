using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VendingMachine : MonoBehaviour
{
    [SerializeField] private float vendingMachineActivationDistance = 1;
    PanelManager panelManager;
    private Player_Movement player;
    private float distance;

    private void Start()
    {
        player = FindObjectOfType<Player_Movement>();
        panelManager = FindObjectOfType<PanelManager>();
    }

    private void VendingMachineShop()
    {
        if (panelManager.vendingPanel != null)
        {
            distance = Vector2.Distance(this.transform.position, player.transform.position);

            if (distance < vendingMachineActivationDistance)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    panelManager.vendingPanel.SetActive(true);
                }
            }
        }
    }

    private void Update()
    {
        VendingMachineShop();
    }
}
