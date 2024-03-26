using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 3f;
    private int shieldLevel;
    
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isSprinting = false;
    private bool usingShield = false;
    private float shieldTimer = 0f;
    //default shield duration is 5 seconds
    private float shieldDuration = 5f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void OnMovement(InputAction.CallbackContext ctxt)
    {
        movement = ctxt.ReadValue<Vector2>();

        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("X", movement.x);
            animator.SetFloat("Y", movement.y);
        }
        else
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsSprinting", false);
            animator.SetBool("ShieldWalking", false);
            animator.SetBool("ShieldSprinting", false);
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

    public void OnShield(InputAction.CallbackContext ctxt){
        //still need to add a logic that decreases the shield count if press

        if (ctxt.performed)
        {
            //testing purposes
            // PlayerPrefs.SetInt("ShieldLevel", 1);
            // PlayerPrefs.Save();
            usingShield = true;
            Debug.Log("Shield Activated");
            animator.SetBool("UsingShield", true);
            
            //get shield level
            shieldLevel = PlayerPrefs.GetInt("ShieldLevel", 1);
            animator.SetInteger("ShieldLevel", shieldLevel);
            // Calculate delay based on shieldLevelIndex
            shieldTimer = shieldDuration + (2f * (shieldLevel - 1));
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsSprinting", false);
        }
    }


    private void FixedUpdate()
    {
        shieldTimer -= Time.fixedDeltaTime; // Decrease shield timer

        //check if shield is got any seconds left;
        if(shieldTimer <= 0f){
            usingShield = false;
            animator.SetBool("UsingShield", false);
            animator.SetBool("ShieldSprinting", false);
            animator.SetBool("ShieldWalking", false);
        }

        if (usingShield){
            //change speed;
            speed = 2f;
            //conditions
            if (isSprinting){
                if (movement.x != 0 || movement.y != 0)
                {
                    animator.SetBool("ShieldSprinting", true);
                    rb.MovePosition(rb.position + movement * (speed*2) * Time.fixedDeltaTime);
                }
                else
                {
                    animator.SetBool("ShieldSprinting", false);
                }
            }
            else{
                if (movement.x != 0 || movement.y != 0)
                {
                    animator.SetBool("ShieldWalking", true);
                    rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

                }
                else
                {
                    animator.SetBool("ShieldWalking", false);
                }
            }
        }
        else{
            //default speed
            speed = 3f;
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
}
