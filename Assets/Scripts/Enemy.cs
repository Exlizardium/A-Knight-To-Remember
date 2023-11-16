using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Assets;

public class Enemy : MonoBehaviour, IDamageController
{
    private Player_Movement player;
    private Rigidbody2D enemyRigidbody;
    [SerializeField] private ParticleSystem bloodSplash;
    [SerializeField] private GameObject booze;
    private AudioManager enemySound;

    [SerializeField] private float pushForce = 1;
    private Vector2 placeToSpawnBooze;
    public float enemyHealth;
    public bool damaged;
    private Vector3 offset = new Vector3(0, 0, -1);


    void Start()
    {
        enemyRigidbody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player_Movement>();
        enemySound = FindObjectOfType<AudioManager>();
    }
    
    public void GetDamaged(float d)
    {
        enemyHealth -= d;
        damaged = true;
        enemySound.Play("Enemy damaged");
        enemyRigidbody.AddForce((this.transform.position - player.transform.position) * (this.transform.position - player.transform.position).magnitude * pushForce, ForceMode2D.Impulse);
        StartCoroutine(SlowDownTimer());

        Instantiate(bloodSplash, this.transform.position + offset, Quaternion.identity);
    }

    IEnumerator SlowDownTimer()
    {
        yield return new WaitForSeconds(1);
        enemyRigidbody.velocity = Vector3.zero;
    }

    private void OnDeath()
    {
        if (enemyHealth <= 0)
        {
            Instantiate(bloodSplash, this.transform.position + offset, Quaternion.identity);
            enemySound.Play("Enemy death");
            Destroy(this.gameObject);

            if (booze != null)
            {
                Instantiate(booze, placeToSpawnBooze, Quaternion.identity);
            }
        }
    }

    void Update()
    {
        placeToSpawnBooze = this.transform.position;
        OnDeath();
    }
}
