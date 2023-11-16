using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Assets;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Rendering;
using TMPro;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float dashTime;
    public float boozeTime = 100f;
    public float health = 100f;
    public float mana = 100f;
    public float maxMana = 100f;
    [SerializeField] public float numberOfKeysObtained = 0;
    public float chanceToGetMimic = 0;
    public bool pointerVisible = true;
    public bool secondLife = false;

    [SerializeField] private Camera playerCamera;
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private Collider2D playerCollider;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] PanelManager panelManager;
    [SerializeField] private SpriteRenderer playerRenderer;
    private AudioManager playerAudio;

    [SerializeField] public Image healthbarImage;
    [SerializeField] public Image boozebarImage;
    [SerializeField] private TextMeshProUGUI manaTracker;
    [SerializeField] private TextMeshProUGUI healthTracker;
    [SerializeField] private TextMeshProUGUI boozeTracker;
    [SerializeField] private Text keysObtainedToUI;

    public Volume drunk;

    public Vector3 direction;
    public Vector3 mousePosition;
    Vector3 cameraPosition;

    private void OnEnable()
    {
        Fireball.DealFireDamage += OnTakeDamage;
        Goblin.DealGoblinDamage += OnTakeDamage;
        Mimic.DealMimicDamage += OnTakeDamage;
        Battlemage.DealBattlemageDamage += OnTakeDamage;
        Trap.DealTrapDamage += OnTakeDamage;
    }

    private void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        playerCollider = gameObject.GetComponent<Collider2D>();
        drunk = FindObjectOfType<Volume>();
        panelManager = FindObjectOfType<PanelManager>();
        playerRenderer = GetComponent<SpriteRenderer>();
        playerAudio = FindObjectOfType<AudioManager>();
    }

    private void Move()
    {
        float X = Input.GetAxisRaw("Horizontal");
        float Y = Input.GetAxisRaw("Vertical");

        direction.x = X;
        direction.y = Y;

        playerRigidbody.AddForce(direction * speed * Time.deltaTime);
    }

    private void CameraMovement()
    {
        cameraPosition = this.transform.position;
        cameraPosition.z = -10;
        playerCamera.transform.position = cameraPosition;
    }

    private void AnimatedMovement()
    {
        playerAnimator.SetFloat("Horizontal", direction.x);
        playerAnimator.SetFloat("Speed", direction.sqrMagnitude);
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        playerAnimator.SetFloat("lastHorizontalDirection", mousePosition.x);  
    }

    private void OnTakeDamage(float d)
    {
        playerAudio.Play("Enemy attack");
        health -= d;
        healthbarImage.fillAmount = health/100f;
        playerRenderer.color = Color.red;
        playerAudio.Play("Player damaged");
        Invoke("ReturnToOriginalColor", 0.1f);
    }

    private void ReturnToOriginalColor()
    {
        playerRenderer.color = Color.white;
    }

    private void BoozeTimer()
    {
        if (panelManager.panelIsActive == false)
        {
            if (boozeTime > 0)
            {
                StartCoroutine(BoozeCountdown());
                boozeTime -= 1f * Time.deltaTime;
                boozebarImage.fillAmount = boozeTime / 100f;
            }
            if (drunk != null)
            {
                drunk.weight = 1f - boozebarImage.fillAmount;
            }
        }
    }

    IEnumerator BoozeCountdown()
    {
        yield return new WaitForSeconds(1);
    }

    private void PointerVisibility()
    {
        if (pointerVisible == true)
        {
            if (this.gameObject.transform.GetChild(0).gameObject.activeInHierarchy == false)
            {
                this.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        else if (pointerVisible == false)
        {
            if (this.gameObject.transform.GetChild(0).gameObject.activeInHierarchy == true)
            {
                this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    private void UITrackers()
    {
        int manaTrackerValue = (int) mana;
        int healthTrackerValue = (int)health;
        int boozeTrackerValue = (int)boozeTime;

        manaTracker.text = manaTrackerValue.ToString();
        healthTracker.text = healthTrackerValue.ToString();
        boozeTracker.text = boozeTrackerValue.ToString();

        keysObtainedToUI.text = numberOfKeysObtained.ToString();
    }

    private void Death()
    {
        if (health <= 0)
        {
            if (secondLife == true)
            {
                playerAudio.Play("Revive");
                health = 100;
                secondLife = false;
            }
            else
            {
                panelManager.deathScreen.SetActive(true);
                playerAudio.Stop("Main theme");
                playerAudio.Stop("Boss theme");
                playerAudio.Play("Game over");
            }
        }
    }

    private void OnDisable()
    {
        Fireball.DealFireDamage -= OnTakeDamage;
        Goblin.DealGoblinDamage -= OnTakeDamage;
        Mimic.DealMimicDamage -= OnTakeDamage;
        Battlemage.DealBattlemageDamage -= OnTakeDamage;
        Trap.DealTrapDamage -= OnTakeDamage;
    }

    private void Update()
    { 
        BoozeTimer();
        PointerVisibility();
        UITrackers();
    }

    private void FixedUpdate()
    {
        CameraMovement();
        Move();
        AnimatedMovement();
        Death();
    }
}
