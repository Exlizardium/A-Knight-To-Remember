using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;
using UnityEngine.UI;

public class Bookshelf : MonoBehaviour, IDamageController
{
    [SerializeField] private float bookshelfHealth = 2;
    [SerializeField] private Player_Movement player;
    [SerializeField] private Player_Combat playerCombat;
    [SerializeField] private float manaRestored = 10;
    [SerializeField] private ParticleSystem manaSplash;
    private AudioManager bookshelfAudio;
    private Vector3 offset = new Vector3(0, 0, -1);

    private void Start()
    {
        player = FindObjectOfType<Player_Movement>();
        playerCombat = FindObjectOfType<Player_Combat>();
        bookshelfAudio = FindObjectOfType<AudioManager>();
    }

    public void GetDamaged(float d)
    {
        bookshelfHealth -= d;
        Instantiate(manaSplash, this.transform.position + offset, Quaternion.identity);
        bookshelfAudio.Play("Bookshelf");
    }

    private void BookshelfDestruction()
    {
        if (bookshelfHealth <= 0)
        {
            if(player.mana < player.maxMana)
            {
                player.mana += manaRestored;
            }
            playerCombat.manabarImage.fillAmount = player.mana / 100f;
            Instantiate(manaSplash, this.transform.position + offset, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        BookshelfDestruction();
    }
}
