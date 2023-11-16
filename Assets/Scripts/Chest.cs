using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class Chest : MonoBehaviour, IDamageController
{
    [SerializeField] private float chestHealth = 5;
    private Gold gold;
    public IDamageController DamageController;
    [SerializeField] private ParticleSystem moneySplash;
    [SerializeField] private GameObject treasure;
    [SerializeField] private GameObject mimic;
    [SerializeField] private Player_Movement player;
    private AudioManager chestAudio;
    private Vector3 offset = new Vector3(0, 0, -1);

    private void Start()
    {
        player = FindObjectOfType<Player_Movement>();
        DamageController = this;
        gold = FindObjectOfType<Gold>();
        chestAudio = FindObjectOfType<AudioManager>();
    }

    public void GetDamaged(float d)
    {
        chestHealth -= d;
        Instantiate(moneySplash, this.transform.position + offset, Quaternion.identity);
        chestAudio.Play("Gold");
    }

    private void ChestDestruction()
    {
        if (chestHealth <= 0)
        {
            if (Random.Range(0, 100) < player.chanceToGetMimic)
            {
                Instantiate(mimic, this.transform.position, Quaternion.identity);
                player.chanceToGetMimic = 0;
            }
            else
            {
                Instantiate(treasure, this.transform.position, Quaternion.identity);
                gold = FindObjectOfType<Gold>();
                gold.goldPoints += 100;
                chestAudio.Play("Money");
                player.chanceToGetMimic += 20;
            }

            Instantiate(moneySplash, this.transform.position + offset, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        ChestDestruction();
    }
}
