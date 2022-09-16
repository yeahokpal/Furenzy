using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementTemp : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    public Rigidbody2D rb;

    Vector2 moveInput;
    
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
}
