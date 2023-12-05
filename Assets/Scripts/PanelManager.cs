using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class PanelManager : MonoBehaviour
{
    public GameObject shopPanel;
    public GameObject vendingPanel;
    public GameObject loadingScreen;
    public GameObject deathScreen;
    public GameObject victoryScreen;
    public GameObject monkPanel;
    public GameObject traderPanel;
    public GameObject angelPanel;
    public GameObject geniePanel;
    public GameObject tutorialPanel;
    public GameObject fireballTutorialPanel;
    public GameObject interactButtonPrompt;

    public GameObject[] temporaryPanels;

    public bool panelIsActive = false;
    [SerializeField] private GameObject playerUIPanel;
    private Player_Movement player;
    [SerializeField] private GameObject objectThatIsBeingInteractedWith;
    private AudioManager audioManager;

    private void Start()
    {
        shopPanel = GameObject.Find("Shop");
        vendingPanel = GameObject.Find("VendingPanel");
        loadingScreen = GameObject.Find("LoadingScreen");
        playerUIPanel = GameObject.Find("Player UI");
        deathScreen = GameObject.Find("DeathScreen");
        victoryScreen = GameObject.Find("VictoryScreen");
        tutorialPanel = GameObject.Find("Tutorial");
        fireballTutorialPanel = GameObject.Find("Fireball tutorial");

        monkPanel = GameObject.Find("MonkMonologue");
        traderPanel = GameObject.Find("TraderMonologue");
        angelPanel = GameObject.Find("AngelMonologue");
        geniePanel = GameObject.Find("GenieMonologue");

        interactButtonPrompt = GameObject.Find("Interact button");

        temporaryPanels = GameObject.FindGameObjectsWithTag("Temporary panels");

        player = FindObjectOfType<Player_Movement>();
        audioManager = FindObjectOfType<AudioManager>();

        if (shopPanel != null)
        {
            shopPanel.SetActive(false);
        }
        if (vendingPanel != null)
        {
            vendingPanel.SetActive(false);
        }
        if (loadingScreen != null)
        {
            loadingScreen.SetActive(false);
        }
        if (deathScreen != null)
        {
            deathScreen.SetActive(false);
        }
        if (victoryScreen != null)
        {
            victoryScreen.SetActive(false);
        }
        if (monkPanel != null)
        {
            monkPanel.SetActive(false);
        }
        if (traderPanel != null)
        {
            traderPanel.SetActive(false);
        }
        if (angelPanel != null)
        {
            angelPanel.SetActive(false);
        }
        if (geniePanel != null)
        {
            geniePanel.SetActive(false);
        }
        if (fireballTutorialPanel != null)
        {
            fireballTutorialPanel.SetActive(false);
        }
        if(interactButtonPrompt != null)
        {
            interactButtonPrompt.SetActive(false);
        }
    }

    private void CheckVisibilityForGamePause()
    {
        if (shopPanel.activeInHierarchy == true || vendingPanel.activeInHierarchy == true || monkPanel.activeInHierarchy == true || traderPanel.activeInHierarchy == true || angelPanel.activeInHierarchy == true || geniePanel.activeInHierarchy == true)
        {
            Time.timeScale = 0;
            playerUIPanel.SetActive(false);
            Cursor.visible = true;
            panelIsActive = true;
            player.pointerVisible = false;
        }
        else if (loadingScreen.activeInHierarchy == true)
        {
            playerUIPanel.SetActive(false);
            panelIsActive = true;
            player.pointerVisible = false;
            player.drunk.enabled = false;
        }
        else if (deathScreen.activeInHierarchy == true || victoryScreen.activeInHierarchy == true)
        {
            playerUIPanel.SetActive(false);
            Cursor.visible = true;
            panelIsActive = true;
            player.pointerVisible = false;
            StartCoroutine(WaitToPause());
            Time.timeScale = 0;
        }
        else if (tutorialPanel.activeInHierarchy == true || fireballTutorialPanel.activeInHierarchy == true)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            panelIsActive = true;
            player.pointerVisible = false;
        }
        else
        {
            Time.timeScale = 1;
            playerUIPanel.SetActive(true);
            Cursor.visible = false;
            panelIsActive = false;
            player.pointerVisible = true;

            if (player.drunk.enabled == false)
            {
                player.drunk.enabled = true;
            }
        }
    }

    private void TutorialScreen()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (panelIsActive)
            {
                if (temporaryPanels != null)
                {
                    foreach (GameObject panel in temporaryPanels)
                    {
                        if (panel.activeInHierarchy)
                        {
                            panel.SetActive(false);
                        }
                    }
                }
            }

            if (!tutorialPanel.activeInHierarchy)
            {
                Time.timeScale = 0;
                Cursor.visible = true;
                panelIsActive = true;
                player.pointerVisible = false;
                tutorialPanel.SetActive(true);
            }
            else
            {
                tutorialPanel.SetActive(false);
            }
        } 
    }

    private void PromptToInteract()
    {
        GameObject[] interactiveList = GameObject.FindGameObjectsWithTag("Interactive");
        Vector2 playerPosition = player.transform.position;

        foreach (GameObject interactiveElement in interactiveList)
        {
            Vector2 interactiveObjectPosition = interactiveElement.gameObject.transform.position;

            if (Vector2.Distance(interactiveObjectPosition, playerPosition) < 1)
            {
                if (interactButtonPrompt.activeInHierarchy == false)
                {
                    interactButtonPrompt.SetActive(true);
                    objectThatIsBeingInteractedWith = interactiveElement;
                }
            }

            if (objectThatIsBeingInteractedWith != null)
            {
                if (Vector2.Distance(objectThatIsBeingInteractedWith.transform.position, playerPosition) > 2f)
                {
                    if (interactButtonPrompt.activeInHierarchy == true)
                    {
                        interactButtonPrompt.SetActive(false);
                    }
                }
            }
        }
        
    }

    private IEnumerator WaitToPause()
    {
        yield return new WaitForSeconds(2f);
    }

    private void footstepsStopperWhenPanelIsActive()
    {
        if (panelIsActive)
        {
            audioManager.Stop("Footsteps");
        }
    }

    private void Update()
    {
        CheckVisibilityForGamePause();
        TutorialScreen();
        PromptToInteract();
        footstepsStopperWhenPanelIsActive();
    }
}
