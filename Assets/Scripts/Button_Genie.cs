using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Button_Genie : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI declineMessage;
    private Player_Movement player;
    private Player_Combat playerCombat;
    private PanelManager panelManager;
    private Button cursedFireballButton;
    private AudioManager audioManager;
    private bool cursed = false;
    private bool healthbarImageUpdated = false;

    private void Start()
    {
        player = FindObjectOfType<Player_Movement>();
        playerCombat = FindObjectOfType<Player_Combat>();
        panelManager = FindObjectOfType<PanelManager>();
        audioManager = FindObjectOfType<AudioManager>();
        cursedFireballButton = GetComponent<Button>();
    }

    private void CheckIfCursed()
    {
        if (cursed == true)
        {
            cursedFireballButton.interactable = false;
        }
        else
        {
            cursedFireballButton.interactable = true;
        }
    }

    private void CursePlayer()
    {
        if (cursed == false)
        {
            player.health = 1f;

            if (healthbarImageUpdated == false)
            {
                player.healthbarImage.fillAmount = player.health / 100f;
                healthbarImageUpdated = true;
            }

            playerCombat.fireballSkillAcquiered = true;
            cursed = true;
            audioManager.Play("Boss laugh");
            declineMessage.text = "No regrets";
            panelManager.fireballTutorialPanel.SetActive(true);
        }
    }

    private void Update()
    {
        cursedFireballButton.onClick.AddListener(CursePlayer);
        CheckIfCursed();
    }
}
