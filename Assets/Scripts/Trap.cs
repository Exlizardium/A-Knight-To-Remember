using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Trap : MonoBehaviour
{
    [SerializeField] private Animator trapAnimator;
    [SerializeField] private float trapDamage = 10;
    [SerializeField] GameObject trapEffect;
    private AudioManager trapAudio;
    private Player_Movement player;
    private bool dealtDamage = false;

    public static event UnityAction<float> DealTrapDamage;

    private void Start()
    {
        trapAudio = FindObjectOfType<AudioManager>();
        player = FindObjectOfType<Player_Movement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && dealtDamage == false)
        {
            trapAnimator.SetTrigger("Activate");
            trapAudio.Play("Trap");
            Instantiate(trapEffect, this.transform.position, Quaternion.identity);
            DealTrapDamage?.Invoke(trapDamage);
            player.healthbarImage.fillAmount = player.health / 100f;
            dealtDamage = true;
            Invoke("TrapDestruction", 3f);
        }
    }

    private void TrapDestruction()
    {
        Destroy(this.gameObject);
    }
}
