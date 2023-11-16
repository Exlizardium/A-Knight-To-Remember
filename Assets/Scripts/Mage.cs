using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mage : MonoBehaviour
{
    [SerializeField] private Player_Movement player;
    [SerializeField] private Enemy enemy;
    [SerializeField] private Animator mageAnimator;
    private Rigidbody2D mageRigidbody;
    [SerializeField] private float mageSpeed = 1f;
    [SerializeField] private float targetDistance = 1f;
    [SerializeField] private float mageHealth = 5f;

    void Start()
    {
        player = FindObjectOfType<Player_Movement>();
        mageRigidbody = GetComponent<Rigidbody2D>();

        this.enemy.enemyHealth = mageHealth;
    }

    private void MageMove()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < targetDistance)
        {
            mageRigidbody.AddForce((transform.position - player.transform.position) * mageSpeed * Time.deltaTime);
        }
    }

    private void MageAnimation()
    {
        float positionByX = player.transform.position.x - this.transform.position.x;
        mageAnimator.SetFloat("Horizontal", positionByX);
    }

    void Update()
    {
        MageMove();
        MageAnimation();
    }
}
