using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Battlemage : MonoBehaviour
{
    [SerializeField] private Player_Combat player;
    [SerializeField] private Enemy enemy;

    [SerializeField] private float battlemageSpeed = 1f;
    [SerializeField] private float targetDistance = 1f;
    [SerializeField] private float maximumDistanceToChase = 4f;
    private float distance;

    [SerializeField] private float battlemageHealth = 3f;
    [SerializeField] public int battlemageDamage = 1;

    [SerializeField] private Animator battlemageAnimator;
    private Rigidbody2D battlemageRigidbody;


    [SerializeField] private float reloadTime = 0.1f;
    private float reloadTimer;

    public static event UnityAction<float> DealBattlemageDamage;


    private void Start()
    {
        player = FindObjectOfType<Player_Combat>();
        enemy = FindObjectOfType<Enemy>();
        battlemageRigidbody = GetComponent<Rigidbody2D>();
        this.enemy.enemyHealth = battlemageHealth;
    }

    private void BattlemageMove()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance > targetDistance && distance < maximumDistanceToChase)
        {
            battlemageRigidbody.AddForce((player.transform.position - transform.position) * battlemageSpeed * Time.deltaTime);
        }
    }

    private void BattlemageAttack()
    {
        if (player != null && player.shield == false && distance <= targetDistance)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer > 0) return;
            reloadTimer = reloadTime;

            battlemageAnimator.SetTrigger("Attack");
            DealBattlemageDamage?.Invoke(battlemageDamage);
            print("Battlemage smash");
        }
    }

    private void BattlemageAnimation()
    {
        float positionByX = player.transform.position.x - this.transform.position.x;

        battlemageAnimator.SetFloat("Horizontal", positionByX);
        battlemageAnimator.SetFloat("Speed", battlemageRigidbody.velocity.magnitude);
    }

    private void Update()
    {
        BattlemageMove();
        BattlemageAttack();
        BattlemageAnimation();
    }
}
