using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftRoomBoss : MonoBehaviour
{
    private Enemy enemy;
    GameObject[] minionList;
    private Goblin goblin;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
        minionList = GameObject.FindGameObjectsWithTag("LeftBossMinion");
    }

    private void MinionProtection()
    {
        if (enemy.damaged)
        {
            foreach (GameObject minion in minionList)
            {
                if (minion != null)
                {
                    goblin = minion.GetComponent<Goblin>();
                    goblin.goblinRage = true;
                }
            }
        }
    }

    private void Update()
    {
        MinionProtection();
    }
}
