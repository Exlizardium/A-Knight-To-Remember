using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelRoll : MonoBehaviour
{
    [SerializeField] private GameObject rollingBarrel;
    private Player_Movement player;

    private GameObject firstSpawnPoint;
    private GameObject secondSpawnPoint;
    private GameObject thirdSpawnPoint;
    private GameObject fourthSpawnPoint;
    private GameObject fifthSpawnPoint;

    private float firstReloadTime;
    private float firstReloadTimer;
    private float secondReloadTime;
    private float secondReloadTimer;
    private float thirdReloadTime;
    private float thirdReloadTimer;
    private float fourthReloadTime;
    private float fourthReloadTimer;
    private float fifthReloadTime;
    private float fifthReloadTimer;

    private void Start()
    {
        firstSpawnPoint = GameObject.Find("Barrel_spawn_point_1");
        secondSpawnPoint = GameObject.Find("Barrel_spawn_point_2");
        thirdSpawnPoint = GameObject.Find("Barrel_spawn_point_3");
        fourthSpawnPoint = GameObject.Find("Barrel_spawn_point_4");
        fifthSpawnPoint = GameObject.Find("Barrel_spawn_point_5");

        firstReloadTime = Random.Range(1, 5);
        secondReloadTime = Random.Range(1, 5);
        thirdReloadTime = Random.Range(1, 5);
        fourthReloadTime = Random.Range(1, 5);
        fifthReloadTime = Random.Range(1, 5);

        player = FindObjectOfType<Player_Movement>();
    }

    private void RollTheFirstBarrel()
    {
        firstReloadTimer -= Time.deltaTime;
        if (firstReloadTimer > 0) return;
        firstReloadTimer = Random.Range(1, 5);
        Instantiate(rollingBarrel, firstSpawnPoint.transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 90f));
    }

    private void RollTheSecondBarrel()
    {
        secondReloadTimer -= Time.deltaTime;
        if (secondReloadTimer > 0) return;
        secondReloadTimer = Random.Range(1, 5);
        Instantiate(rollingBarrel, secondSpawnPoint.transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 90f));
    }

    private void RollTheThirdBarrel()
    {
        thirdReloadTimer -= Time.deltaTime;
        if (thirdReloadTimer > 0) return;
        thirdReloadTimer = Random.Range(1, 5);
        Instantiate(rollingBarrel, thirdSpawnPoint.transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 90f));
    }

    private void RollTheFourthBarrel()
    {
        fourthReloadTimer -= Time.deltaTime;
        if (fourthReloadTimer > 0) return;
        fourthReloadTimer = Random.Range(1, 5);
        Instantiate(rollingBarrel, fourthSpawnPoint.transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 90f));
    }

    private void RollTheFifthBarrel()
    {
        fifthReloadTimer -= Time.deltaTime;
        if (fifthReloadTimer > 0) return;
        fifthReloadTimer = Random.Range(1, 5);
        Instantiate(rollingBarrel, fifthSpawnPoint.transform.position, transform.rotation * Quaternion.Euler(0f, 0f, 90f));
    }

    private void CheckForPlayerArrivalToStart()
    {
        float distanceToPlayer = Vector2.Distance(this.transform.position, player.transform.position);

        if (distanceToPlayer < 40f)
        {
            RollTheFirstBarrel();
            RollTheSecondBarrel();
            RollTheThirdBarrel();
            RollTheFourthBarrel();
            RollTheFifthBarrel();
        }     
    }

    private void Update()
    {
        CheckForPlayerArrivalToStart();
    }
}
