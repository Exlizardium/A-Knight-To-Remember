using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Goblin : MonoBehaviour
{
    [SerializeField] private Player_Combat player;
    [SerializeField] private Enemy enemy;

    [SerializeField] private float goblinSpeed = 1f;
    [SerializeField] private float targetDistance = 1f;
    [SerializeField] private Animator goblinAnimator;
    private Rigidbody2D goblinRigidbody;
    private AudioManager goblinAudio;

    [SerializeField] private float goblinHealth = 3f;
    [SerializeField] public int goblinDamage = 1;
    public bool goblinRage = false;

    [SerializeField] private float reloadTime = 0.1f;
    private float reloadTimer;

    private float distance;
    private Vector3 lastPosition;

    public static event UnityAction<float> DealGoblinDamage;

    void Start()
    {
        player = FindObjectOfType<Player_Combat>();
        goblinRigidbody = GetComponent<Rigidbody2D>();
        goblinAudio = FindObjectOfType<AudioManager>();
        this.enemy.enemyHealth = goblinHealth;
    }

    private void GoblinMove()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance > targetDistance)
        {
            goblinRigidbody.AddForce((player.transform.position - transform.position) * goblinSpeed * Time.deltaTime);
        }
    }

    private void EnragedCheck()
    {
        if (enemy.damaged)
        {
            goblinRage = true;
        }
    }

    private void GoblinRage()
    {
        if (goblinRage)
        {
            goblinAnimator.SetTrigger("Enrage");
            goblinAudio.Play("Goblin");
            GoblinMove();
            GoblinAttack();
        }     
    }

    private void GoblinAttack()
    {
        if (player != null && player.shield == false && distance <= targetDistance)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer > 0) return;
            reloadTimer = reloadTime;

            DealGoblinDamage?.Invoke(goblinDamage);
            print("Goblin smash");
        }
    }

    private void GoblinAnimation()
    {
        float currentPosition = player.transform.position.x - this.transform.position.x;
        goblinAnimator.SetFloat("Horizontal", currentPosition);
    }

    void Update()
    {
        GoblinAnimation();
        EnragedCheck();
        GoblinRage();
    }
}
