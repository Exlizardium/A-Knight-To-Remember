using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBarrel : MonoBehaviour
{
    [SerializeField] private float barrelRollingSpeed = 5f;

    private void BarrelMovement()
    {
        transform.Translate(Vector3.left * barrelRollingSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Despawn")
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        BarrelMovement();
    }
}
