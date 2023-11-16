using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booze : MonoBehaviour
{
    [SerializeField] private float boozeBonusTime = 10f;
    [SerializeField] private AnimationCurve floatingEffect;
    private AudioManager boozeAudio;
    private Player_Movement player;
    float startingY;
    private bool boozebarImageUpdated = false;

    private void Start()
    {
        player = FindObjectOfType<Player_Movement>();
        boozeAudio = FindObjectOfType<AudioManager>();
        startingY = this.transform.position.y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            boozeAudio.Play("Beer");

            if (player.boozeTime <= 90)
            {
                player.boozeTime += boozeBonusTime;
            }
            else
            {
                player.boozeTime = 100f;
            }

            if (boozebarImageUpdated == false)
            {
                player.boozebarImage.fillAmount = player.boozeTime / 100f;
                boozebarImageUpdated = true;
            }

            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        this.transform.position = new Vector2(this.transform.position.x, startingY + floatingEffect.Evaluate((Time.time % floatingEffect.length)));
    }
}
