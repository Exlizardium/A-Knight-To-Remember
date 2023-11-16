using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button_Angel : MonoBehaviour
{
    private Player_Movement player;
    private Gold gold;
    private Button secondLifeButton;
    private bool blessed = false;
    [SerializeField] private TextMeshProUGUI declineMessage;

    private void Start()
    {
        player = FindObjectOfType<Player_Movement>();
        gold = FindObjectOfType<Gold>();
        secondLifeButton = GetComponent<Button>();
    }

    private void CheckCredibility()
    {
        if (gold.goldPoints < 500 || blessed == true)
        {
            secondLifeButton.interactable = false;
        }
        else
        {
            secondLifeButton.interactable = true;
        }
    }

    private void BlessPlayer()
    {
        if (gold.goldPoints >= 500 && blessed == false)
        {
            gold.goldPoints -= 500;
            player.secondLife = true;
            blessed = true;
            declineMessage.text = "Thanks, you're an angel!";
        }
    }

    private void Update()
    {
        CheckCredibility();
        secondLifeButton.onClick.AddListener(BlessPlayer);
    }
}
