using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mimic : MonoBehaviour
{
    [SerializeField] private Player_Combat player;
    [SerializeField] private Enemy enemy;

    [SerializeField] private float mimicSpeed = 1f;
    [SerializeField] private float targetDistance = 1f;
    [SerializeField] private Animator mimicAnimator;
    [SerializeField] private Rigidbody2D mimicRigidbody;

    [SerializeField] private float mimicHealth = 10f;
    [SerializeField] public int mimicDamage = 3;

    [SerializeField] private float reloadTime = 1f;
    private float reloadTimer;

    private float distance;
    private Vector3 lastPosition;

    public static event UnityAction<float> DealMimicDamage;


    void Start()
    {
        player = FindObjectOfType<Player_Combat>();
        mimicRigidbody = GetComponent<Rigidbody2D>();

        this.enemy.enemyHealth = mimicHealth;
    }

    private void MimicMove()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance > targetDistance)
        {
            mimicRigidbody.AddForce((player.transform.position - transform.position) * mimicSpeed * Time.deltaTime);
        }
    }

    private void MimicAttack()
    {
        if (player != null && player.shield == false && distance <= targetDistance)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer > 0) return;
            reloadTimer = reloadTime;

            DealMimicDamage?.Invoke(mimicDamage);
        }
    }

    private void MimicAnimation()
    {
        float currentSpeed = (this.transform.position - lastPosition).magnitude;
        float currentPosition = player.transform.position.x - this.transform.position.x;
        lastPosition = this.transform.position;

        mimicAnimator.SetFloat("Horizontal", currentPosition);
        mimicAnimator.SetFloat("Speed", currentSpeed);
    }

    void Update()
    {
        MimicMove();
        MimicAttack();
        MimicAnimation();
    }
}
