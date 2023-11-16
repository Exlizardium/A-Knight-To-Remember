using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private float targetDistanceToPlayer = 3f;
    [SerializeField] private float keySpeed = 3f;
    Player_Movement player;
    [SerializeField] private Animator keyAnimator;
    Vector2 lastPosition = Vector2.zero;
    private Rigidbody2D keyRigidbody;
    private AudioManager keyAudio;

    private void Start()
    {
        player = FindObjectOfType<Player_Movement>();
        keyRigidbody = GetComponent<Rigidbody2D>();
        keyAudio = FindObjectOfType<AudioManager>();
    }

    private void CheckForPlayerToRunAway()
    {
        float distanceToPlayer = Vector2.Distance(this.transform.position, player.transform.position);
        if(distanceToPlayer < targetDistanceToPlayer)
        {
            keyRigidbody.AddForce((transform.position - player.transform.position) * keySpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.GetComponent<Player_Movement>() != null)
        {
            player.numberOfKeysObtained++;
            keyAudio.Play("Key");
            Destroy(this.gameObject);
        } 
    }

    private void KeyAnimation()
    {
        keyAnimator.SetFloat("Horizontal", keyRigidbody.transform.position.x);
        keyAnimator.SetFloat("Speed", keyRigidbody.velocity.magnitude);
    }

    private void Update()
    {
        CheckForPlayerToRunAway();
        KeyAnimation();
    }
}
