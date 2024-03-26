using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShieldMovement : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    
    private Vector2 lastPosition;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isSprinting = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void SetLastPosition (Vector2 position){
        lastPosition = position;
    }

    public void OnMovement(InputAction.CallbackContext ctxt)
    {
        movement = ctxt.ReadValue<Vector2>();
        movement = lastPosition - movement;

        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("X", movement.x);
            animator.SetFloat("Y", movement.y);
        }
        else
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsSprinting", false);
        }
    }

    public void OnSprint(InputAction.CallbackContext ctxt)
    {
        if (ctxt.action.triggered && ctxt.ReadValue<float>() > 0)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
            animator.SetBool("IsSprinting", false);
        }
    }

    private void FixedUpdate()
    {
        if (isSprinting)
        {
            if (movement.x != 0 || movement.y != 0)
            {
                animator.SetBool("IsSprinting", true);
                rb.MovePosition(rb.position + movement * (speed * 2) * Time.fixedDeltaTime);
            }
            else
            {
                animator.SetBool("IsSprinting", false);
            }
        }
        else
        {
            if (movement.x != 0 || movement.y != 0)
            {
                animator.SetBool("IsWalking", true);
                rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
            }
            else
            {
                animator.SetBool("IsWalking", false);
            }
        }
    }
}
