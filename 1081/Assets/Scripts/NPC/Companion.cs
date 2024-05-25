using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.AI;

public class Companion : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float targetPosition;
    [SerializeField] Transform playerTransform;
    Rigidbody2D rb;
    Animator aiAnim;
    Transform target;


    void Start(){
        rb = GetComponent<Rigidbody2D>();
        aiAnim = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update(){
        TargetFollow();
    }

    void TargetFollow(){
        if (Vector2.Distance(transform.position, target.position) > targetPosition){
            aiAnim.SetBool("isWalk", true);
            // rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            MoveTowardsPoint(target.position);
        }
        else{
            aiAnim.SetBool("isWalk", false);
            rb.velocity = Vector2.zero;
            // rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
    }

    IEnumerator Go (){
        yield return new WaitForSeconds (2f);
        aiAnim.SetBool("isWalk", true);
        MoveTowardsPoint(target.position);
    }

    void MoveTowardsPoint(Vector2 targetPosition){
        // Calculate the direction to move towards the target position
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

            // Move the enemy towards the target position
            rb.velocity = direction * speed;

            aiAnim.SetFloat("X", direction.x);
            aiAnim.SetFloat("Y", direction.y);
    }
}
