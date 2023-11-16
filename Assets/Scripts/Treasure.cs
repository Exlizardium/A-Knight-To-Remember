using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treasure : MonoBehaviour
{
    private void Start()
    {
        Invoke("FadeAway", 2f);
    }

    private void FadeAway()
    {
        Destroy(this.gameObject);
    }

}
