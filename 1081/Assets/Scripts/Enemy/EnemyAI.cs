using System.Collections;
using System.Collections.Generic;
// using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    public List<GameObject> pathPoints; // List of waypoints defining the path
    public float speed = 4f; // Speed of movement
    public bool isRunning = false;
    public bool caughtPlayer {get; private set;}

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public Direction CurrentDirection { get; private set; } = Direction.Up; // Current movement direction
    public Direction LastDirection { get; private set; } = Direction.Up; // Last movement direction

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
    public bool? waitTime;
    private float startTime;
    public Failed failed;
    public EndTrigger endTrigger;
    public GameObject QMark;

    public AudioSource ShieldDeflect;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        fov = GetComponent<FieldOfView>();
        playerManager = player.GetComponent<PlayerManager>();
        caughtPlayer = false;
        visitedLastPosition = false;
        foundAnim = foundAnimation.GetComponent<Animator>();
        QMark.gameObject.SetActive(false);
        int chapterLevel = GetChapterNum();
        speed = speed + (chapterLevel * 0.2f);
    }

    int GetChapterNum(){
        // Get the active scene name
        string sceneName = SceneManager.GetActiveScene().name;

        // Find the first digit in the string
        foreach (char c in sceneName)
        {
            if (char.IsDigit(c))
            {
                return int.Parse(c.ToString());
            }
        }

        // Return a default value if no digit is found
        return -1;
    }

    void FixedUpdate()
    {
        // Check if there are any waypoints defined
        if (!caughtPlayer){
            GameObject selectedCharacter = playerManager.playerPrefabs[playerManager.characterIndex];
            PlayerMovement playerMovement = playerManager.playerPrefabs[playerManager.characterIndex].GetComponent<PlayerMovement>();
            if (pathPoints.Count == 0)
                return;

                // Check if the enemy is in the same position as the player
            if (Vector2.Distance(transform.position, playerMovement.center.transform.position) < 0.3f)
            {
                if(playerMovement.usingShield){
                    var dir = center.position - playerMovement.center.transform.position;
                    knockBack = true;
                    Vector2 deflectionDirection1 = new Vector2(0f, dir.y);
                    deflectionDirection1.Normalize();
                    Vector2 deflectionDirection2 = new Vector2(dir.x, 0f);
                    deflectionDirection2.Normalize();
                    ShieldDeflect.Play();
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
                    caughtPlayer = true;
                    rb.velocity = Vector2.zero;
                    // Freeze the y position
                    rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                    playerMovement.animator.SetBool("isArrested", true);
                    // Debug.Log("GameOver!");
                    foundAnim.SetBool("found", false);
                    visitedLastPosition = true;
                }
            }
            else{
                caughtPlayer = false;
            }
            if (fov.CanSeePlayer){
                // Move towards player
                if (endTrigger.Win == false){
                    rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
                    MoveTowardsPoint(playerMovement.center.position);
                    lastPosition = playerMovement.center.position;
                    anim.SetBool("isFound", true);
                    foundAnim.SetBool("found", true);
                    isRunning = true;
                    visitedLastPosition = true;
                    LastDirection = CurrentDirection;
                }
                else{
                    isRunning = false;
                    visitedLastPosition = false;
                }
            }
            else{
                isRunning = false; 
                anim.SetBool("isFound", false);
                foundAnim.SetBool("found", false);
                // Move towards the current waypoint
                if (!visitedLastPosition){
                    MoveTowardsPoint(pathPoints[currentPointIndex].transform.position);
                }
                else{
                    if (waitTime != null){
                        WaitTime();
                        if(waitTime == true){
                            rb.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
                            anim.SetBool("isIdle", false);
                            visitedLastPosition = false;
                            waitTime = null;
                        }
                    }
                    else{
                        if (Vector2.Distance(transform.position, lastPosition) < 0.3f){
                            anim.SetBool("isIdle", true);
                            rb.velocity = Vector2.zero;
                            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
                            ZeroAnim();
                            CurrentDirection = LastDirection; 
                            switch (CurrentDirection){
                                case Direction.Up:
                                    anim.SetFloat("X", 0);
                                    anim.SetFloat("Y", 1);
                                    break;
                                case Direction.Down:
                                    anim.SetFloat("X", 0);
                                    anim.SetFloat("Y", -1);
                                    break;
                                case Direction.Left:
                                    anim.SetFloat("X", -1);
                                    anim.SetFloat("Y", 0);
                                    break;
                                case Direction.Right:
                                    anim.SetFloat("X", 1);
                                    anim.SetFloat("Y", 0);
                                    break;
                                default:
                                    anim.SetFloat("X", 0);
                                    anim.SetFloat("Y", 0);
                                    break;
                            }
                            startTime = 5f;
                            WaitTime();
                        }
                        else{
                            MoveTowardsPoint(lastPosition);
                        }
                    }
                }
                // Check if the enemy has reached the current waypoint
                if (Vector2.Distance(transform.position, pathPoints[currentPointIndex].transform.position) < 0.1f)
                {
                    // Move to the next waypoint
                    currentPointIndex = (currentPointIndex + 1) % pathPoints.Count;
                }
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
                    foundAnim.SetBool("found", false);
                    StartCoroutine(Failed());
                }
            }
        }
    }

    private IEnumerator Failed(){
        yield return new WaitForSeconds(0.5f);
        failed.loss=true;
    }
    private IEnumerator UnknockBack(){
        yield return new WaitForSeconds(0.5f);
        knockBack = false;
    }

    private void WaitTime(){
        if (startTime >= 0){
            QMark.gameObject.SetActive(true);
            startTime -= Time.deltaTime;
            waitTime = false;
            if (startTime <= 3f && startTime >= 1.5f){
                fov.idleScan = true;
            }
            else if (startTime <= 1.5f){
                fov.idleScan = false;
            }
        }
        else{
            QMark.gameObject.SetActive(false);
            waitTime = true;
            fov.idleScan = null;
        }
    }

    private void ZeroAnim(){
        anim.SetFloat("X", 0); // Reset X and Y animation parameters
        anim.SetFloat("Y", 0);
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
    }

    void UpdateDirection(Vector2 direction)
    {
        float absX = Mathf.Abs(direction.x);
        float absY = Mathf.Abs(direction.y);

        if (absX > absY)
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
