using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Events;

public class FoxManager : MonoBehaviour
{
    public static FoxManager instance;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private GameObject FireBall;
    [SerializeField] private GameObject Lightning;
    private InputActionMap playerInput;

    public Rigidbody2D rb;
    public Animator animator;
    public int moveDir;
    public int Health = 3;
    public float mana = 1f;
    bool canAttack = true;
    public GameObject currentHealthSprite;
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;
    public GameObject health4;
    public GameObject foxFill;

    public UnityEvent OnDeath;

    Vector2 moveInput;

    private void Awake()
    {
        instance = this;

        health1 = GameObject.Find("FoxHealth1");
        health2 = GameObject.Find("FoxHealth2");
        health3 = GameObject.Find("FoxHealth3");
        health4 = GameObject.Find("FoxHealth4");
        foxFill = GameObject.Find("FoxFill");

        currentHealthSprite = health1;
    }

    void FixedUpdate()
    {
        // Filling the mana bar appropriately
        foxFill.GetComponent<Image>().fillAmount = mana;

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

    public void OnFireBall()
    {
        if (foxFill.GetComponent<Image>().fillAmount >= .5 && Time.timeScale == 1f && canAttack)
        {
            animator.SetTrigger("FireBall");
            if (moveDir == 1)
                Instantiate(FireBall, transform.position, Quaternion.Euler(0f, 0f, 180f));
            else if (moveDir == 2)
                Instantiate(FireBall, transform.position, Quaternion.Euler(0f, 0f, 90f));
            else if (moveDir == 3)
                Instantiate(FireBall, transform.position, Quaternion.Euler(0f, 0f, 0f));
            else if (moveDir == 4)
                Instantiate(FireBall, transform.position, Quaternion.Euler(0f, 0f, -90f));

            mana -= .5f;
            canAttack = false;
            StartCoroutine(Cooldown());
        }
    }

    public void OnLightning()
    {
        if (Time.timeScale == 1f && canAttack)
        {
            animator.SetTrigger("FireBall");
            if (moveDir == 1)
                Instantiate(Lightning, transform.position, Quaternion.Euler(0f, 0f, 180f));
            else if (moveDir == 2)
                Instantiate(Lightning, transform.position, Quaternion.Euler(0f, 0f, 90f));
            else if (moveDir == 3)
                Instantiate(Lightning, transform.position, Quaternion.Euler(0f, 0f, 0f));
            else if (moveDir == 4)
                Instantiate(Lightning, transform.position, Quaternion.Euler(0f, 0f, -90f));
            canAttack = false;
            StartCoroutine(Cooldown());
        }
    }

    public void TakeDamage(int damage)
    {
        gameObject.GetComponent<ParticleSystem>().Play();
        Health = Health - damage;
        if (Health == 0)
            StartCoroutine(Dead());
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
    IEnumerator Dead()
    {
        yield return new WaitForSeconds(.2f);
        gameObject.SetActive(false);
        OnDeath.Invoke();
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
        mana += .1;
    }
}
