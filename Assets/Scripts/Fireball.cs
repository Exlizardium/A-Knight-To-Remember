using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fireball : MonoBehaviour
{
    [SerializeField] private Rigidbody2D fireballRigidbody;
    [SerializeField] private Player_Combat playerCombat;
    [SerializeField] private Player_Movement playerMovement;
    private AudioManager fireballAudio;
    [SerializeField] private float lifespanTime = 5f;
    [SerializeField] private float fireballSpeed = 5f;
    public int fireballDamage = 1;

    public static event UnityAction<float> DealFireDamage;

    private void Start()
    {       
        fireballRigidbody = FindObjectOfType<Rigidbody2D>();
        playerCombat = FindObjectOfType<Player_Combat>();
        playerMovement = FindObjectOfType<Player_Movement>();
        fireballAudio = FindObjectOfType<AudioManager>();

        Launch();

        fireballDamage = Random.Range(7, 10);
    }

    private void Launch()
    {
        fireballRigidbody.AddForce(((Vector2)playerMovement.transform.position - (Vector2)this.transform.position) * fireballSpeed, ForceMode2D.Impulse);
    }

    IEnumerator FireballLifespan()
    {
        yield return new WaitForSeconds(lifespanTime);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.transform.GetComponent<Player_Movement>() != null && playerCombat.shield == false)
        {
            fireballAudio.Play("Fireball");
            DealFireDamage?.Invoke(fireballDamage);
            print("Fireball collided");
        }

        if (collision.transform.GetComponent<Shoot>() == null && collision.transform.GetComponent<Bookshelf>() == null && collision.transform.GetComponent<Fireball>() == null && collision.transform.GetComponent<Trap>() == null)
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        StartCoroutine(FireballLifespan());     
    }
}
