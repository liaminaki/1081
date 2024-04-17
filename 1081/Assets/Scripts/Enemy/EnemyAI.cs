using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public List<GameObject> pathPoints; // List of waypoints defining the path
    public float speed = 3f; // Speed of movement
    public bool isRunning = false;
    public bool caughtPlayer {get; private set;}

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public Direction CurrentDirection { get; private set; } = Direction.Right; // Current movement direction

    private Rigidbody2D rb;
    private Animator anim;
    private int currentPointIndex = 0; // Index of the current waypoint

    private FieldOfView fov;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        fov = GetComponent<FieldOfView>();
    }

    void Update()
    {
        // Check if there are any waypoints defined
        if (pathPoints.Count == 0)
            return;

        if (fov.CanSeePlayer){
            // Move towards player
            if (caughtPlayer){
                rb.velocity = Vector2.zero;
            }
            else{
                MoveTowardsPoint(fov.playerRef.transform.position);
                anim.SetBool("isFound", true);
                isRunning = true;
            }
        }
        else{
            anim.SetBool("isFound", false);
            isRunning = false;
            // Move towards the current waypoint
            MoveTowardsPoint(pathPoints[currentPointIndex].transform.position);

            // Check if the enemy has reached the current waypoint
            if (Vector2.Distance(transform.position, pathPoints[currentPointIndex].transform.position) < 0.1f)
            {
                // Move to the next waypoint
                currentPointIndex = (currentPointIndex + 1) % pathPoints.Count;
            }
        }

            // Check if the enemy is in the same position as the player
        if (Vector2.Distance(transform.position, fov.playerRef.transform.position) < 0.1f)
        {
            // The enemy is in the same position as the player
            // You can add your logic here
            anim.SetBool("isIdle", true);
            caughtPlayer = true;
            rb.velocity = Vector2.zero;
            Debug.Log("GameOver!");
        }
        else{
            caughtPlayer = false;
        }
    }

    void MoveTowardsPoint(Vector2 targetPosition)
    {
        // Calculate the direction to move towards the target position
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        UpdateDirection(direction);

        // Move the enemy towards the target position
        if (!isRunning)
            rb.velocity = direction * speed;
        else
            rb.velocity = direction * (speed * 2);

        // Update the animation based on movement direction
        anim.SetFloat("X", direction.x);
        anim.SetFloat("Y", direction.y);
    }

    void UpdateDirection(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
                CurrentDirection = Direction.Right;
            else
                CurrentDirection = Direction.Left;
        }
        else
        {
            if (direction.y > 0)
                CurrentDirection = Direction.Up;
            else
                CurrentDirection = Direction.Down;
        }
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
