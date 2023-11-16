using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private float reloadTime = 3f;
    [SerializeField] private float shotRange = 2f;
    private Player_Movement player;

    private GameObject fireball;
    private float reloadTimer;

    private void Start()
    {
        player = FindObjectOfType<Player_Movement>();
    }

    private void CreateFireball()
    {
        float distance = Vector2.Distance(player.transform.position, this.transform.position);

        if (distance <= shotRange)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer > 0) return;
            reloadTimer = reloadTime;

            fireball = transform.GetChild(0).gameObject;
            GameObject newFire = Instantiate(fireball, transform.parent);
            newFire.transform.position = this.transform.position;
            newFire.SetActive(true);
        } 
    }

    void Update()
    {
        CreateFireball();
    }
}
