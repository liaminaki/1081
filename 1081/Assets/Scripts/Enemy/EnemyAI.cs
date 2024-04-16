using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject PointA;
    public GameObject PointB;

    private Rigidbody2D rb;
    private Animator anim;
    private Transform currentPoint;
    public float speed;

    void Start (){
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentPoint = PointB.transform;
    }

    void Update (){
        Vector2 point = currentPoint.position - transform.position;
        if (currentPoint == PointB.transform){
            rb.velocity = new Vector2(speed, 0);
        }
        else{
            rb.velocity = new Vector2(-speed, 0);
        }

        float xVelocity = rb.velocity.x;
        float yVelocity = rb.velocity.y;
        anim.SetFloat("X", xVelocity);
        anim.SetFloat("Y", yVelocity);

        if (Vector2.Distance(transform.position, currentPoint.position) < 1f && currentPoint == PointB.transform){
            currentPoint = PointA.transform;
        }
        if (Vector2.Distance(transform.position, currentPoint.position) < 1f && currentPoint == PointA.transform){
            currentPoint = PointB.transform;
        }
    }

    private void OnDrawGizmos(){
        Gizmos.DrawWireSphere(PointA.transform.position, 1f);
        Gizmos.DrawWireSphere(PointB.transform.position, 1f);
        Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);
    }
}
