using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private InputActionMap playerInput;

    public Rigidbody2D rb;
    public Animator animator;
    public int moveDir;

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

        // Defining variables used by Animator
        animator.SetInteger("MoveDir", moveDir);
        animator.SetFloat("Speed", moveInput.magnitude);
        animator.SetFloat("Horizontal", moveInput.x);
        animator.SetFloat("Vertical", moveInput.y);

    }

    public void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
