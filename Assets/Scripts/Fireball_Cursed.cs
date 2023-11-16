using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball_Cursed : MonoBehaviour
{
    private Rigidbody2D fireballRigidbody;
    private Player_Combat playerCombat;

    [SerializeField] private float lifespanTime = 5;
    [SerializeField] private float fireballSpeed = 10;
    private AudioManager fireballAudio;

    private void Start()
    {
        fireballRigidbody = GetComponent<Rigidbody2D>();
        playerCombat = FindObjectOfType<Player_Combat>();
        fireballAudio = FindObjectOfType<AudioManager>();

        Launch();
    }

    private void Launch()
    {
        fireballRigidbody.AddForce(((Vector2)playerCombat.attackPointerNew - (Vector2)this.transform.position) * fireballSpeed, ForceMode2D.Impulse);
    }

    IEnumerator fireballLifespan()
    {
        yield return new WaitForSeconds(lifespanTime);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.GetComponent<Enemy>() != null)
        {
            fireballAudio.Play("Fireball");
            collision.transform.GetComponent<Enemy>().GetDamaged(playerCombat.fireballDamage);
        }

        if (collision.transform.GetComponent<Player_Movement>() == null && collision.transform.GetComponent<Bookshelf>() == null && collision.transform.GetComponent<Fireball>() == null && collision.transform.GetComponent<Trap>() == null)
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        StartCoroutine(fireballLifespan());
    }
}
