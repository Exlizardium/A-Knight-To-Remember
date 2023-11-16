using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player_Combat : MonoBehaviour
{
    public float playerDamage = 1f;
    public float attackRange = 1f;
    public float shieldDuration = 1f;
    public float fireballDamage = 5f;
    public bool shield = false;
    [SerializeField] private float pointerPositionProximity = 1;
    [SerializeField] private float manaCost = 10;
    [SerializeField] private float fireballManaCost = 20;

    [SerializeField] private Animator playerAnimator;
    [SerializeField] PanelManager panelManager;
    private AudioManager playerCombatAudio;

    [SerializeField] private GameObject attackPointer;
    [SerializeField] private GameObject shieldVisual;
    [SerializeField] private GameObject cursedFireball;
    public Image manabarImage;
    public bool fireballSkillAcquiered = false;

    RaycastHit2D HitObject = new RaycastHit2D();

    private Player_Movement player;
    private Vector2 alternativeDirection;
    public Vector3 attackPointerNew;

    private void Start()
    {
        player = FindObjectOfType<Player_Movement>();
        panelManager = FindObjectOfType<PanelManager>();
        playerCombatAudio = FindObjectOfType<AudioManager>();

        shieldVisual.gameObject.SetActive(false);
        Cursor.visible = false;
    }
    
    private void LocateTarget()
    {
        LayerMask layerMask = 1 << 8;
        layerMask = ~layerMask;
        Ray2D ray = new Ray2D();
        
        alternativeDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition) - this.transform.position;
        alternativeDirection.Normalize();
        ray.origin = this.transform.position;
        ray.direction = alternativeDirection;
    
        HitObject = Physics2D.Raycast(ray.origin, ray.direction, attackRange, layerMask);
        Debug.DrawRay(ray.origin, ray.direction * attackRange, Color.red);

        attackPointerNew.x = this.transform.position.x + ray.direction.x * attackRange;
        attackPointerNew.y = this.transform.position.y + ray.direction.y * attackRange;
        attackPointerNew.z = pointerPositionProximity;

        attackPointer.transform.position = attackPointerNew;
    }

    private void DamageTarget()
    {
        if (Input.GetMouseButtonDown(0) && panelManager.panelIsActive == false)
        {
            playerAnimator.SetTrigger("Attack");
            playerCombatAudio.Play("Slash");

            if (HitObject.transform != null)
            {
                playerCombatAudio.Play("Strike");

                Chest chest = HitObject.transform.GetComponent<Chest>();
                Explosion explosive = HitObject.transform.GetComponent<Explosion>();
                Bookshelf bookshelf = HitObject.transform.GetComponent<Bookshelf>();
                
                if (HitObject.transform.TryGetComponent<Enemy>(out Enemy enemy))
                {
                    enemy.GetDamaged(playerDamage);
                }
                else if (chest != null)
                {
                    chest.GetDamaged(playerDamage);
                }
                else if (explosive != null)
                {
                    explosive.GetDamaged(playerDamage);
                }
                else if (bookshelf != null)
                {
                    bookshelf.GetDamaged(playerDamage);
                }
            }
        }
    }

    private void Block()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (player.mana >= manaCost)
            {
                player.mana -= manaCost;
                manabarImage.fillAmount = player.mana / 100f;

                shield = true;
                shieldVisual.gameObject.SetActive(true);
                playerCombatAudio.Play("Shield");

                Invoke("BlockExit", shieldDuration);
            }
        }
    }

    private void BlockExit()
    {
        shield = false;
        shieldVisual.gameObject.SetActive(false);
    }

    private void CursedFireball()
    {
        if(fireballSkillAcquiered == true)
        {
            if (Input.GetMouseButtonDown(1) && panelManager.panelIsActive == false && player.mana >= fireballManaCost)
            {
                Instantiate(cursedFireball, this.transform.position, Quaternion.identity);
                player.mana -= fireballManaCost;
                manabarImage.fillAmount = player.mana / 100f;
            }
        }
    }

    private void Update()
    {
        LocateTarget();
        DamageTarget();
        Block();
        CursedFireball();
    }
}
