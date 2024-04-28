using System.Collections;
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
    public GameObject player;
    public PlayerManager playerManager;
    public float knockBackForce = 6f;
    [SerializeField] private Transform center;
    [SerializeField] private bool knockBack;
    private Vector2 lastPosition;
    public bool visitedLastPosition {get; private set;}
    public GameObject foundAnimation;
    public Animator foundAnim;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        fov = GetComponent<FieldOfView>();
        playerManager = player.GetComponent<PlayerManager>();
        caughtPlayer = false;
        visitedLastPosition = false;
        foundAnimation.SetActive(false);
        foundAnim = foundAnimation.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        // Check if there are any waypoints defined
        if (!caughtPlayer){
            GameObject selectedCharacter = playerManager.playerPrefabs[playerManager.characterIndex];
            PlayerMovement playerMovement = playerManager.playerPrefabs[playerManager.characterIndex].GetComponent<PlayerMovement>();
            if (pathPoints.Count == 0)
                return;

            if (fov.CanSeePlayer){
                // Move towards player
                MoveTowardsPoint(playerMovement.center.position);
                lastPosition = playerMovement.center.position;
                anim.SetBool("isFound", true);
                foundAnimation.SetActive(true);
                isRunning = true;
                visitedLastPosition = true;
            }
            else{
                isRunning = false; 
                anim.SetBool("isFound", false);
                foundAnimation.SetActive(false);
                // Move towards the current waypoint
                if (!visitedLastPosition){
                    MoveTowardsPoint(pathPoints[currentPointIndex].transform.position);
                }
                else{
                    if (Vector2.Distance(transform.position, lastPosition) < 0.1f){
                        StartCoroutine(ScanArea());
                    }
                    else{
                        MoveTowardsPoint(lastPosition);
                    }
                }
                // Check if the enemy has reached the current waypoint
                if (Vector2.Distance(transform.position, pathPoints[currentPointIndex].transform.position) < 0.1f)
                {
                    // Move to the next waypoint
                    currentPointIndex = (currentPointIndex + 1) % pathPoints.Count;
                }
            }

                // Check if the enemy is in the same position as the player
            if (Vector2.Distance(transform.position, playerMovement.center.transform.position) < 0.1f)
            {
                if(playerMovement.usingShield){
                    var dir = center.position - playerMovement.center.transform.position;
                    knockBack = true;
                    Vector2 deflectionDirection1 = new Vector2(0f, dir.y);
                    deflectionDirection1.Normalize();
                    Vector2 deflectionDirection2 = new Vector2(dir.x, 0f);
                    deflectionDirection2.Normalize();
                    switch (CurrentDirection){
                        case Direction.Up:
                            rb.velocity = deflectionDirection1 * knockBackForce;
                            break;
                        case Direction.Down:
                            rb.velocity = deflectionDirection1 * knockBackForce;
                            break;
                        case Direction.Left:
                            rb.velocity = deflectionDirection2 * knockBackForce;
                            break;
                        case Direction.Right:
                            rb.velocity = deflectionDirection2 * knockBackForce;
                            break;
                        default:
                            break;
                    }
                    StartCoroutine(UnknockBack());
                }
                else{
                    isRunning = false;
                    anim.SetBool("isIdle", true);
                    caughtPlayer = true;
                    rb.velocity = Vector2.zero;
                    // Freeze the y position
                    rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                    Debug.Log("GameOver!");
                }
            }
            else{
                caughtPlayer = false;
            }
        }
        else{
            isRunning = false;
            anim.SetBool("isIdle", true);
            rb.velocity = Vector2.zero;
            // Freeze the y position
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
            if (playerManager != null){
                PlayerMovement playerMovement = playerManager.playerPrefabs[playerManager.characterIndex].GetComponent<PlayerMovement>();
                if (playerMovement != null){
                    playerMovement.ArrestedState();
                    playerManager.TurnOffPlayerInput();
                }
            }
        }
    }

    private IEnumerator UnknockBack(){
        yield return new WaitForSeconds(0.5f);
        knockBack = false;
    }

    private IEnumerator ScanArea(){
        anim.SetBool("isIdle", true);
        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        yield return new WaitForSeconds(3f);
        rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
        anim.SetBool("isIdle", false);
        visitedLastPosition = false;
    }

    void MoveTowardsPoint(Vector2 targetPosition)
    {
        // Calculate the direction to move towards the target position
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        UpdateDirection(direction);

        if (!knockBack){
            // Move the enemy towards the target position
            if (!isRunning)
                rb.velocity = direction * speed;
            else
                rb.velocity = direction * (speed * 3);

            anim.SetFloat("X", direction.x);
            anim.SetFloat("Y", direction.y);
        }
        // else{
        //     var lerpedVelocityX = Mathf.Lerp(rb.velocity.x, 0f, Time.deltaTime);
        //     var lerpedVelocityY = Mathf.Lerp(0f, rb.velocity.y, Time.deltaTime);
        //     if (CurrentDirection == Direction.Right || CurrentDirection == Direction.Left){
        //         rb.velocity = new Vector2 (lerpedVelocityX, rb.velocity.y);
        //     }
        //     else if (CurrentDirection == Direction.Up || CurrentDirection == Direction.Down){
        //         rb.velocity = new Vector2 (rb.velocity.x, lerpedVelocityY);
        //     }
        // }
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
        // if (lastPosition != null){
        //     Gizmos.color = Color.red;
        //     Gizmos.DrawLine(transform.position,lastPosition);
        // }
    }
}
