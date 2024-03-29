/*
 * Programmer: Jack / Caden
 * Purpose: Manages user inputs and calls actions from them
 * Input: Player inputs
 * Output: Player actions
 */

using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    #region Variables

    //Player Stats
    public int Health = 3;
    public float mana = 1f;
    bool canSearchForUI = true;
    public GameObject currentHealthSprite;
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;
    public GameObject health4;
    public GameObject healthFill;
    public UnityEvent OnDeath;

    //Knockback / Hitstun
    public static PlayerManager instance;
    public float KnockbackPower = 100;
    public float KnockbackDuration = 1;

    //Movement
    [SerializeField] private float moveSpeed = 5f;
    public Rigidbody2D rb;
    Vector2 moveInput;
    bool canMove = true;

    //Animations
    public Animator animator;
    public int moveDir = 3;

    //Audio
    public AudioSource audioSource;
    public AudioClip Hit;
    public AudioClip Hurt;
    public AudioClip Shoot;

    //Abilities / Attacks
    bool canAttack = true;
    [SerializeField] private GameObject FireBall;
    [SerializeField] private GameObject Lightning;
    public GameObject Arrow;
    public GameObject Knife;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int StabDamage = 2;

    // Other
    EventLog eventLog;

    #endregion

    #region Default Methods
    private void Start()
    {
        eventLog = GameObject.Find("SaveManager").GetComponent<EventLog>();
        GameObject.Find("SaveManager").GetComponent<EventLog>().enabled = false;
        audioSource = gameObject.GetComponent<AudioSource>();
        instance = this;

        currentHealthSprite = health1;
    }

    private void Update()
    {
        switch (gameObject.name)
        {
            case "Fox(Clone)":
                if (healthFill == null)
                    healthFill = GameObject.Find("FoxFill");
                if (health1 == null)
                    health1 = GameObject.Find("FoxHealth1");
                if (health2 == null)
                    health2 = GameObject.Find("FoxHealth2");
                if (health3 == null)
                    health3 = GameObject.Find("FoxHealth3");
                if (health4 == null)
                    health4 = GameObject.Find("FoxHealth4");
                break;
            case "Bunny(Clone)":
                if (healthFill == null)
                    healthFill = GameObject.Find("BunnyFill");
                if (health1 == null)
                    health1 = GameObject.Find("BunnyHealth1");
                if (health2 == null)
                    health2 = GameObject.Find("BunnyHealth2");
                if (health3 == null)
                    health3 = GameObject.Find("BunnyHealth3");
                if (health4 == null)
                    health4 = GameObject.Find("BunnyHealth4");
                break;
            case "Bird(Clone)":
                if (healthFill == null)
                    healthFill = GameObject.Find("BirdFill");
                if (health1 == null)
                    health1 = GameObject.Find("BirdHealth1");
                if (health2 == null)
                    health2 = GameObject.Find("BirdHealth2");
                if (health3 == null)
                    health3 = GameObject.Find("BirdHealth3");
                if (health4 == null)
                    health4 = GameObject.Find("BirdHealth4");
                break;
            case "Ferret(Clone)":
                if (healthFill == null)
                    healthFill = GameObject.Find("FerretFill");
                if (health1 == null)
                    health1 = GameObject.Find("FerretHealth1");
                if (health2 == null)
                    health2 = GameObject.Find("FerretHealth2");
                if (health3 == null)
                    health3 = GameObject.Find("FerretHealth3");
                if (health4 == null)
                    health4 = GameObject.Find("FerretHealth4");
                break;
        }

        FindHealthUI();
        if (canMove)
        {
            rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
        }

        // Filling the mana bar appropriately
        if (mana > 1f)
            mana = 1f;
        healthFill.GetComponent<Image>().fillAmount = mana;

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

        // Defining variables used by Animator
        animator.SetInteger("MoveDir", moveDir);
        animator.SetFloat("Speed", moveInput.magnitude);
        animator.SetFloat("Horizontal", moveInput.x);
        animator.SetFloat("Vertical", moveInput.y);

        // Health and UI enabling
        switch (Health)
        {
            case 3:
                currentHealthSprite = health1;
                health2.SetActive(false);
                health3.SetActive(false);
                health4.SetActive(false);
                break;
            case 2:
                currentHealthSprite = health2;
                health1.SetActive(false);
                health3.SetActive(false);
                health4.SetActive(false);
                break;
            case 1:
                currentHealthSprite = health3;
                health1.SetActive(false);
                health2.SetActive(false);
                health4.SetActive(false);
                break;
            case 0:
                currentHealthSprite = health4;
                health1.SetActive(false);
                health2.SetActive(false);
                health3.SetActive(false);
                Dead();
                break;
        }
        currentHealthSprite.SetActive(true);

        GetComponentInParent<Transform>().position = gameObject.transform.position;
    }

    #endregion

    #region Custom Methods

    #region Attacking
    // Fox
    public void OnFireBall()
    {
        if (healthFill.GetComponent<Image>().fillAmount >= .5 && Time.timeScale == 1f && canAttack)
        {
            eventLog.AddEvent("Attack: Fireball");
            animator.SetTrigger("FireBall");
            if (moveDir == 1)
                Instantiate(FireBall, transform.position + new Vector3(0, .75f, 0), Quaternion.Euler(0f, 0f, 180f));
            else if (moveDir == 2)
                Instantiate(FireBall, transform.position + new Vector3(.75f, 0, 0), Quaternion.Euler(0f, 0f, 90f));
            else if (moveDir == 3)
                Instantiate(FireBall, transform.position + new Vector3(0, -.75f, 0), Quaternion.Euler(0f, 0f, 0f));
            else if (moveDir == 4)
                Instantiate(FireBall, transform.position + new Vector3(-.75f, 0, 0), Quaternion.Euler(0f, 0f, -90f));
            mana -= .5f;
            canAttack = false;
            audioSource.clip = Shoot;
            audioSource.Play();
            StartCoroutine(Cooldown());
        }
    }
    public void OnLightning()
    {
        if (Time.timeScale == 1f && canAttack)
        {
            eventLog.AddEvent("Attack: Lightning");
            animator.SetTrigger("FireBall");
            if (moveDir == 1)
                Instantiate(Lightning, transform.position + new Vector3(0, .75f, 0), Quaternion.Euler(0f, 0f, 180f));
            else if (moveDir == 2)
                Instantiate(Lightning, transform.position + new Vector3(.75f, 0, 0), Quaternion.Euler(0f, 0f, 90f));
            else if (moveDir == 3)
                Instantiate(Lightning, transform.position + new Vector3(0, -1, 0), Quaternion.Euler(0f, 0f, 0f));
            else if (moveDir == 4)
                Instantiate(Lightning, transform.position + new Vector3(-.75f, 0, 0), Quaternion.Euler(0f, 0f, -90f));
            canAttack = false;
            StartCoroutine(Cooldown());
            audioSource.clip = Shoot;
            audioSource.Play();
        }
    }
    // Bunny
    public void OnStab()
    {
        if (canAttack)
        {
            eventLog.AddEvent("Attack: Stab");
            //Play Attack Animation
            switch (moveDir)
            {
                case 1:
                    animator.SetTrigger("AttackNorth");
                    attackPoint.position = GetComponentInParent<Transform>().position + new Vector3(0f, 0.5f, 0f);
                    break;
                case 2:
                    animator.SetTrigger("AttackEast");
                    attackPoint.position = GetComponentInParent<Transform>().position + new Vector3(0.5f, 0.0f, 0f);
                    break;
                case 3:
                    animator.SetTrigger("AttackSouth");
                    attackPoint.position = GetComponentInParent<Transform>().position + new Vector3(0f, -0.5f, 0f);
                    break;
                case 4:
                    animator.SetTrigger("AttackWest");
                    attackPoint.position = GetComponentInParent<Transform>().position + new Vector3(-0.5f, 0.0f, 0f);
                    break;
            }
            audioSource.clip = Shoot; // Add melee sound
            audioSource.Play();

            StartCoroutine(Cooldown());

            //Hit Detection
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            //Deal Damage
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy is not CircleCollider2D)
                {
                    enemy.GetComponent<EnemyTarget>().TakeDamage(StabDamage);
                    enemy.GetComponent<EnemyTarget>().HitStunWait(.5f);
                    MP_Up();
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
        eventLog.AddEvent("Attack: Throw Knife");
        if (Time.timeScale == 1f && canAttack && mana >= .5f)
        {
            //animator.SetTrigger("Knife");
            if (moveDir == 1)
                Instantiate(Knife, transform.position + new Vector3(0, 1f, 0), Quaternion.Euler(0f, 0f, 180f));
            else if (moveDir == 2)
                Instantiate(Knife, transform.position + new Vector3(1f, 0, 0), Quaternion.Euler(0f, 0f, 90f));
            else if (moveDir == 3)
                Instantiate(Knife, transform.position + new Vector3(0, -1f, 0), Quaternion.Euler(0f, 0f, 0f));
            else if (moveDir == 4)
                Instantiate(Knife, transform.position + new Vector3(-1f, 0, 0), Quaternion.Euler(0f, 0f, -90f));
            StartCoroutine(Cooldown());

            mana -= .5f;

            audioSource.clip = Shoot;
            audioSource.Play();
        }
    }
    // Bird
    public void OnShootOne()
    {
        eventLog.AddEvent("Attack: Shoot Arrow");
        if (canAttack)
        {
            switch (moveDir)
            {
                case 1:
                    Instantiate(Arrow, transform.position + new Vector3(0, 1f, 0), Quaternion.Euler(0f, 0f, 180f));
                    break;
                case 2:
                    Instantiate(Arrow, transform.position + new Vector3(.75f, 0, 0), Quaternion.Euler(0f, 0f, 90f));
                    break;
                case 3:
                    Instantiate(Arrow, transform.position + new Vector3(0, -1f, 0), Quaternion.Euler(0f, 0f, 0f));
                    break;
                case 4:
                    Instantiate(Arrow, transform.position + new Vector3(-.75f, 0, 0), Quaternion.Euler(0f, 0f, -90f));
                    break;
            }
            StartCoroutine(Cooldown());
            audioSource.clip = Shoot;
            audioSource.Play();
        }
    }
    // Ferret
    private void OnBonk()
    {
        if (canAttack)
        {
            animator.SetTrigger("Bonk");
            attackRange = 1f;
            switch (moveDir)
            {
                case 1:
                    attackPoint.position = GetComponentInParent<Transform>().position + new Vector3(0f, 0.75f, 0f);
                    break;
                case 2:
                    attackPoint.position = GetComponentInParent<Transform>().position + new Vector3(0.75f, 0.0f, 0f);
                    break;
                case 3:
                    attackPoint.position = GetComponentInParent<Transform>().position + new Vector3(0f, -0.75f, 0f);
                    break;
                case 4:
                    attackPoint.position = GetComponentInParent<Transform>().position + new Vector3(-0.75f, 0.0f, 0f);
                    break;
            }
            audioSource.clip = Shoot; // Add melee sound
            audioSource.Play();

            StartCoroutine(Cooldown());

            //Hit Detection
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            //Deal Damage
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy is not CircleCollider2D)
                {
                    enemy.GetComponent<EnemyTarget>().TakeDamage(2);
                    MP_Up();
                }
            }
        }
    }

    private void OnSpin()
    {
        if (canAttack && mana >= .5f)
        {
            animator.SetTrigger("Spin");
            attackRange = 2.5f;
            attackPoint.position = GetComponentInParent<Transform>().position + new Vector3(0f, 0f, 0f);
            audioSource.clip = Shoot; // Add melee sound
            audioSource.Play();

            StartCoroutine(Cooldown());

            //Hit Detection
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

            //Deal Damage
            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy is not CircleCollider2D)
                {
                    enemy.GetComponent<EnemyTarget>().TakeDamage(3);
                    MP_Up();
                }
            }
            mana -= .5f;
        }
    }
    // All
    public void MP_Up()
    {
        mana += .1f;
    }
    IEnumerator Cooldown()
    {
        canAttack = false;
        yield return new WaitForSeconds(.33f);
        canAttack = true;
    }
    #endregion

    #region Movement / UI

    public void FindHealthUI()
    {
        if (canSearchForUI)
        {
            switch (gameObject.name)
            {
                case "Fox(Clone)":
                    health1 = GameObject.Find("FoxHealth1");
                    health2 = GameObject.Find("FoxHealth2");
                    health3 = GameObject.Find("FoxHealth3");
                    health4 = GameObject.Find("FoxHealth4");
                    healthFill = GameObject.Find("FoxFill");
                    break;
                case "Bunny(Clone)":
                    health1 = GameObject.Find("BunnyHealth1");
                    health2 = GameObject.Find("BunnyHealth2");
                    health3 = GameObject.Find("BunnyHealth3");
                    health4 = GameObject.Find("BunnyHealth4");
                    healthFill = GameObject.Find("BunnyFill");
                    break;
                case "Bird(Clone)":
                    health1 = GameObject.Find("BirdHealth1");
                    health2 = GameObject.Find("BirdHealth2");
                    health3 = GameObject.Find("BirdHealth3");
                    health4 = GameObject.Find("BirdHealth4");
                    healthFill = GameObject.Find("BirdFill");
                    break;
                case "Ferret(Clone)":
                    health1 = GameObject.Find("FerretHealth1");
                    health2 = GameObject.Find("FerretHealth2");
                    health3 = GameObject.Find("FerretHealth3");
                    health4 = GameObject.Find("FerretHealth4");
                    healthFill = GameObject.Find("FerretFill");
                    break;
            }
            canSearchForUI = false;
        }
    }
    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    public IEnumerator OnDash()
    {
        if (mana >= .5f)
        {
            mana -= .5f;
            rb.AddForce(moveInput * 10, ForceMode2D.Impulse);
            canMove = false;
            yield return new WaitForSeconds(.4f);
            canMove = true;
            rb.velocity = new Vector2(0f, 0f);
        }
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
    private void OnEventLog()
    {
        if (GameObject.Find("SaveManager").GetComponent<EventLog>().enabled == true)
            GameObject.Find("SaveManager").GetComponent<EventLog>().enabled = false;
        else
            GameObject.Find("SaveManager").GetComponent<EventLog>().enabled = true;
    }
    #endregion

    #region Health
    private void Dead()
    {
        int numOfActivePlayers = 0;
        if (GameObject.Find("Fox(Clone)"))
            ++numOfActivePlayers;
        if (GameObject.Find("Bunny(Clone)"))
            ++numOfActivePlayers;
        if (GameObject.Find("Bird(Clone)"))
            ++numOfActivePlayers;
        if (GameObject.Find("Ferret(Clone)"))
            ++numOfActivePlayers;
        if (numOfActivePlayers == 0)
        {
            if (GameObject.Find("P1_Menu_Controls(Clone)"))
                Destroy(GameObject.Find("P1_Menu_Controls(Clone)"));
            if (GameObject.Find("P2_Menu_Controls(Clone)"))
                Destroy(GameObject.Find("P2_Menu_Controls(Clone)"));
            if (GameObject.Find("P3_Menu_Controls(Clone)"))
                Destroy(GameObject.Find("P3_Menu_Controls(Clone)"));
            if (GameObject.Find("P4_Menu_Controls(Clone)"))
                Destroy(GameObject.Find("P4_Menu_Controls(Clone)"));
            GameObject.Find("LevelManager").GetComponent<LevelManager>().GameOver();
        }
            

        gameObject.SetActive(false);
    }
    public void TakeDamage(int damage)
    {
        gameObject.GetComponent<ParticleSystem>().Play();
        Health = Health - damage;
        audioSource.clip = Hurt;
        audioSource.Play();
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
    #endregion

    #endregion
}