using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAnchorTrigger : MonoBehaviour
{
    public bool isTriggered { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //this.GetComponent<BoxCollider2D>().autoTiling = true;
        isTriggered = true;
    }
}
