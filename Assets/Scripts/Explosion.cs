using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets;

public class Explosion : MonoBehaviour, IDamageController
{
    [SerializeField] private float explosionForce = 5f;
    [SerializeField] private float explosionRadius = 10f;
    [SerializeField] private float explosiveHP = 1;
    [SerializeField] private float timeBeforeDestruction = 1;
    [SerializeField] private float shopActivationDistance = 1;
    private float distance;

    [SerializeField] Vector3 explosionDirection = new Vector3(1, 1);
    [SerializeField] Rigidbody2D playerRigidbody;
    [SerializeField] Rigidbody2D enemyRigidbody;
    [SerializeField] Rigidbody2D stationRigidbody;

    [SerializeField] private Animator stationAnimator;
    [SerializeField] PanelManager panelManager;
    private Player_Movement player;

    private void Start()
    {
        player = FindObjectOfType<Player_Movement>();
        panelManager = FindObjectOfType<PanelManager>();
    }

    public void GetDamaged(float d)
    {
        explosiveHP -= d;
    }

    private void AddExplosionForce(Rigidbody2D explosiveRigidbody)
    {
        Vector2 direction = explosiveRigidbody.transform.position - this.transform.position;
        float relativePush = explosionRadius / direction.magnitude + 0.1f;
     
        explosiveRigidbody.AddForce(direction * explosionForce * relativePush * Time.deltaTime);
    }

    private void Explode()
    {
        if (explosiveHP <= 0)
        {
            RaycastHit2D[] explodedList;
            explodedList = Physics2D.CircleCastAll(this.transform.position, explosionRadius, explosionDirection);

            foreach (RaycastHit2D element in explodedList)
            {
                if (element.rigidbody != null && element.rigidbody != stationRigidbody)
                {
                    AddExplosionForce(element.rigidbody);
                }

                if (element.transform.gameObject.tag == "Barrel")
                {
                    Destroy(element.transform.gameObject);
                }
            }

            stationAnimator.SetTrigger("Explode");
            StartCoroutine(DelayExplosion());
            
        }
    }

    IEnumerator DelayExplosion()
    {
        yield return new WaitForSeconds(timeBeforeDestruction);
        Destroy(this.gameObject);
    }

    private void AlchemyShop()
    {
        if(panelManager.shopPanel != null)
        {
            distance = Vector2.Distance(this.transform.position, player.transform.position);

            if (distance < shopActivationDistance)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    panelManager.shopPanel.SetActive(true);
                }
            }
        }
    }

    private void Update()
    {
        Explode();
        if (panelManager != null)
        {
            AlchemyShop();
        }
    }
}
