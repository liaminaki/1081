// using System.Collections;
// using System.Collections.Generic;
// using System.Drawing;
// using UnityEngine;

// public class EnemyAI : MonoBehaviour
// {
//     public GameObject PointA;
//     public GameObject PointB;

//     private Rigidbody2D rb;
//     private Animator anim;
//     private Transform currentPoint;
//     public float speed;

//     public bool isVertical;

//     void Start (){
//         rb = GetComponent<Rigidbody2D>();
//         anim = GetComponent<Animator>();
//         currentPoint = PointB.transform;
//     }

//     void Update (){
//         Vector2 point = currentPoint.position - transform.position;

//         if (currentPoint == PointB.transform){
//             if (isVertical)
//                 rb.velocity = new Vector2(0, speed);
//             else
//                 rb.velocity = new Vector2(speed, 0);
//         }
//         else{
//             if (isVertical)
//                 rb.velocity = new Vector2(0, -speed);
//             else
//                 rb.velocity = new Vector2(-speed, 0);
//         }

//         float xVelocity = rb.velocity.x;
//         float yVelocity = rb.velocity.y;
//         anim.SetFloat("X", xVelocity);
//         anim.SetFloat("Y", yVelocity);

//         if (Vector2.Distance(transform.position, currentPoint.position) < 1f && currentPoint == PointB.transform){
//             currentPoint = PointA.transform;
//         }
//         if (Vector2.Distance(transform.position, currentPoint.position) < 1f && currentPoint == PointA.transform){
//             currentPoint = PointB.transform;
//         }
//     }

//     private void OnDrawGizmos(){
//         Gizmos.DrawWireSphere(PointA.transform.position, 1f);
//         Gizmos.DrawWireSphere(PointB.transform.position, 1f);
//         Gizmos.DrawLine(PointA.transform.position, PointB.transform.position);
//     }
// }

using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public List<GameObject> pathPoints; // List of waypoints defining the path
    public float speed = 5f; // Speed of movement

    private Rigidbody2D rb;
    private Animator anim;
    private int currentPointIndex = 0; // Index of the current waypoint

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if there are any waypoints defined
        if (pathPoints.Count == 0)
            return;

        // Move towards the current waypoint
        MoveTowardsPoint(pathPoints[currentPointIndex].transform.position);

        // Check if the enemy has reached the current waypoint
        if (Vector2.Distance(transform.position, pathPoints[currentPointIndex].transform.position) < 0.1f)
        {
            // Move to the next waypoint
            currentPointIndex = (currentPointIndex + 1) % pathPoints.Count;
        }
    }

    void MoveTowardsPoint(Vector2 targetPosition)
    {
        // Calculate the direction to move towards the target position
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // Move the enemy towards the target position
        rb.velocity = direction * speed;

        // Update the animation based on movement direction
        anim.SetFloat("X", direction.x);
        anim.SetFloat("Y", direction.y);
    }

    // Visualize the path in the editor
    private void OnDrawGizmos()
    {
        if (pathPoints.Count < 2)
            return;

        // Draw lines between waypoints to visualize the path
        for (int i = 0; i < pathPoints.Count - 1; i++)
        {
            Gizmos.DrawLine(pathPoints[i].transform.position, pathPoints[i + 1].transform.position);
        }

        // Draw a line between the last and first waypoint to close the path loop
        Gizmos.DrawLine(pathPoints[pathPoints.Count - 1].transform.position, pathPoints[0].transform.position);
    }
}
