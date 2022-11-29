using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BunnyManager : MonoBehaviour
{
    //player stats
    [SerializeField] private float moveSpeed = 5f;
    public int Health = 3;
    public float mana = 1f;

    //movement and animations
    public int moveDir;
    public Rigidbody2D rb;
    private InputActionMap playerInput;
    public Animator animator;
    bool canAttack = true;
    public static BunnyManager instance;
    Vector2 moveInput;

    //UI
    public GameObject currentHealthSprite;
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;
    public GameObject health4;

    //attacks
    public GameObject Knife;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int StabDamage = 3;

    public float KnockbackPower = 100;
    public float KnockbackDuration = 1;


    private void Awake()
    {
        instance = this;

        health1 = GameObject.Find("BunnyHealth1");
        health2 = GameObject.Find("BunnyHealth2");
        health3 = GameObject.Find("BunnyHealth3");
        health4 = GameObject.Find("BunnyHealth4");

        currentHealthSprite = health1;
    }

    void FixedUpdate()
    {
        // Finding the current facing direction
        // North = 1, East = 2, South = 3, West = 4
        if (moveInput.x > .25 && moveInput.y < .25)
            moveDir = 2;
        else if (moveInput.x < -.25 && moveInput.y < .25)
            moveDir = 4;
        else if (moveInput.x < .25 && moveInput.y > .25)
            moveDir = 1;
        else if (moveInput.x < .25 && moveInput.y < -.25)
            moveDir = 3;

        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);

        // Defining variables used by Animator
        animator.SetInteger("MoveDir", moveDir);
        animator.SetFloat("Speed", moveInput.magnitude);
        animator.SetFloat("Horizontal", moveInput.x);
        animator.SetFloat("Vertical", moveInput.y);

        // Cursed Health and UI enabling
        if (Health == 3)
        {
            currentHealthSprite = health1;
            health2.SetActive(false);
            health3.SetActive(false);
            health4.SetActive(false);
        }
        else if (Health == 2)
        {
            currentHealthSprite = health2;
            health1.SetActive(false);
            health3.SetActive(false);
            health4.SetActive(false);
        }
        else if (Health == 1)
        {
            currentHealthSprite = health3;
            health1.SetActive(false);
            health2.SetActive(false);
            health4.SetActive(false);
        }
        else
        {
            health1.SetActive(false);
            health2.SetActive(false);
            health3.SetActive(false);
            health4.SetActive(true);
            currentHealthSprite = health4;
            Dead();
        }

        currentHealthSprite.SetActive(true);
    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public void OnStab()
    {
        if (canAttack)
        {
            //Play Attack Animation
            if (moveDir == 1)
            {
                animator.SetTrigger("AttackNorth");
                attackPoint.position = GetComponentInParent<Transform>().position + new Vector3(0f, 0.5f, 0f);
            }
                
            else if (moveDir == 2)
            {
                animator.SetTrigger("AttackEast");
                attackPoint.position = GetComponentInParent<Transform>().position + new Vector3(0.5f, 0.0f, 0f);
            }
                
            else if (moveDir == 3)
            {
                animator.SetTrigger("AttackSouth");
                attackPoint.position = GetComponentInParent<Transform>().position + new Vector3(0f, -0.5f, 0f);
            }
                
            else if (moveDir == 4)
            {
                animator.SetTrigger("AttackWest");
                attackPoint.position = GetComponentInParent<Transform>().position + new Vector3(-0.5f, 0.0f, 0f);
            }
                
            StartCoroutine(Cooldown());

            //Hit Detection
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            //Deal Damage
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy is not CircleCollider2D)
                {
                    enemy.GetComponent<EnemyTarget>().TakeDamage(StabDamage);
                    StartCoroutine(enemy.GetComponent<EnemyTarget>().HitStun(1f));
                }
            }
        }        
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void OnKnife()
    {
        if (Time.timeScale == 1f && canAttack)
        {
            //animator.SetTrigger("Knife");
            if (moveDir == 1)
                Instantiate(Knife, transform.position + new Vector3(0, .75f, 0), Quaternion.Euler(0f, 0f, 180f));
            else if (moveDir == 2)
                Instantiate(Knife, transform.position + new Vector3(.75f, 0, 0), Quaternion.Euler(0f, 0f, 90f));
            else if (moveDir == 3)
                Instantiate(Knife, transform.position + new Vector3(0, -.75f, 0), Quaternion.Euler(0f, 0f, 0f));
            else if (moveDir == 4)
                Instantiate(Knife, transform.position + new Vector3(-.75f, 0, 0), Quaternion.Euler(0f, 0f, -90f));
            StartCoroutine(Cooldown());
        }
    }
    public void TakeDamage(int damage)
    {
        Health = Health - damage;
    }

    public IEnumerator Knockback(float KnockbackDuration, float KnockbackPower, Transform obj)
    {
        float timer = 0;

        while (KnockbackDuration > timer)
        {
            timer += Time.deltaTime;
            Vector2 direction = (obj.transform.position - this.transform.position).normalized;
            rb.AddForce(-direction * KnockbackPower);
        }

        yield return 0;
    }

    void Dead()
    {
        GameObject.Find("PlayerInputManager").GetComponent<CharacterManager>().Player2 = null;
        gameObject.SetActive(false);
    }
    private void OnPause()
    {
        var CanvasManager = GameObject.Find("UI Elements").GetComponent<CanvasManager>();
        bool isPaused = GameObject.Find("UI Elements").GetComponent<CanvasManager>().isPaused;
        if (isPaused)
            CanvasManager.Resume();
        else
            CanvasManager.Pause();
    }
    IEnumerator Cooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(.33f);
        canAttack = true;
    }

    public void MP_Up()
    {
        mana += .1f;
    }
}
