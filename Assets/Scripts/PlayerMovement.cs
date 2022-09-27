using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    private InputActionMap playerInput;
    public int moveDir;
    public float Health = 3;
    public GameObject currentHealthSprite;
    public GameObject health1;
    public GameObject health2;
    public GameObject health3;
    public GameObject health4;

    Vector2 moveInput;

    void FixedUpdate()
    {
        // Finding the current facing direction
        // North = 1, East = 2, South = 3, West = 4
        if (moveInput.x > .0001 && moveInput.y < .0001)
            moveDir = 2;
        else if (moveInput.x < -.0001 && moveInput.y < .0001)
            moveDir = 4;
        else if (moveInput.x < .0001 && moveInput.y > .0001)
            moveDir = 1;
        else if (moveInput.x < .0001 && moveInput.y < -.0001)
            moveDir = 3;

            rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);

        // Animation Stuff
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

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void TakeDamage()
    {
        Health -= 1;
    }
    void Dead()
    {

    }
}
