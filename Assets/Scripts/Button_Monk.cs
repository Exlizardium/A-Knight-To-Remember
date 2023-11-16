using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button_Monk : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI declineMessage;
    private Player_Movement player;
    private Button enlightenButton;
    private bool enlightened = false;

    private void Start()
    {
        player = FindObjectOfType<Player_Movement>();
        enlightenButton = GetComponent<Button>();
    }

    private void CheckIfDrunk()
    {
        if (player.boozeTime > 0 || enlightened == true)
        {
            enlightenButton.interactable = false;
        }
        else
        {
            enlightenButton.interactable = true;
        }
    }

    private void EnlightenPlayer()
    {
        if (player.boozeTime <= 0 && enlightened == false)
        {         
            player.maxMana = 200;
            player.mana = 200;
            enlightened = true;
            declineMessage.text = "Peace be with you";
        }
    }

    private void Update()
    {
        enlightenButton.onClick.AddListener(EnlightenPlayer);
        CheckIfDrunk();
    }
}
