using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius = 5f;
    [Range(1, 360)] public float angle = 180f;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;

    public GameObject playerRef;

    public bool CanSeePlayer { get; private set; }

    private EnemyAI enemyAI;
    // private LineRenderer lineRenderer;

    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        if (playerObjects.Length > 0)
        {
            playerRef = playerObjects[0];
        }

        // // Add LineRenderer component
        // lineRenderer = gameObject.AddComponent<LineRenderer>();
        // lineRenderer.startWidth = 0.1f;
        // lineRenderer.endWidth = 0.1f;
        // lineRenderer.startColor = Color.yellow;
        // lineRenderer.endColor = Color.yellow;
        // lineRenderer.positionCount = 3;

        StartCoroutine(FOVCheck());
    }

    private IEnumerator FOVCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            FOV();
        }
    }

    private void FOV()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        if (rangeCheck.Length > 0)
        {
            foreach (var collider in rangeCheck)
            {
                Transform target = collider.transform;
                // Get the direction of the enemy AI
                Vector2 enemyDirection = Vector2.zero;
                if (enemyAI != null)
                {
                    switch (enemyAI.CurrentDirection)
                    {
                        case EnemyAI.Direction.Up:
                            enemyDirection = Vector2.up;
                            break;
                        case EnemyAI.Direction.Down:
                            enemyDirection = Vector2.down;
                            break;
                        case EnemyAI.Direction.Left:
                            enemyDirection = Vector2.left;
                            break;
                        case EnemyAI.Direction.Right:
                            enemyDirection = Vector2.right;
                            break;
                        default:
                            break;
                    }
                }
                Vector2 directionToTarget = ((Vector2)target.position - (Vector2)transform.position).normalized;
                 float angleToTarget = Vector2.Angle(enemyDirection, directionToTarget);
                if (angleToTarget < angle / 2)
                {
                    float distanceToTarget = Vector2.Distance(transform.position, target.position);
                    // Check for obstructions
                    if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer))
                        CanSeePlayer = true;
                    else
                        CanSeePlayer = false;
                }
                else
                    CanSeePlayer = false;
            }
        }
        else if (CanSeePlayer)
            CanSeePlayer = false;
    }

    // private void Update()
    // {
    //     if (enemyAI != null){
    //         // Update LineRenderer positions
    //         Vector3 angle01 = DirectionFromAngle(-GetAngleFromDirection(enemyAI.CurrentDirection) - angle / 2);
    //         Vector3 angle02 = DirectionFromAngle(-GetAngleFromDirection(enemyAI.CurrentDirection) + angle / 2);
    //         lineRenderer.SetPosition(0, transform.position);
    //         lineRenderer.SetPosition(1, transform.position + angle01 * radius);
    //         lineRenderer.SetPosition(2, transform.position + angle02 * radius);
    //     }
    // }

    private void OnDrawGizmos()
    {
        if (enemyAI == null){
            // Ensure enemyAI is initialized before accessing it
            Start();
            if (enemyAI == null)
            {
                Debug.LogWarning("EnemyAI reference is not set.");
                return;
            }
        }
        Gizmos.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, radius);

        Vector3 angle01 = DirectionFromAngle(-GetAngleFromDirection(enemyAI.CurrentDirection) - angle / 2);
        Vector3 angle02 = DirectionFromAngle(-GetAngleFromDirection(enemyAI.CurrentDirection) + angle / 2);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + angle01 * radius);
        Gizmos.DrawLine(transform.position, transform.position + angle02 * radius);

        if (CanSeePlayer)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, playerRef.transform.position);
        }
    }

    private Vector2 DirectionFromAngle(float angleInDegrees)
    {
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    private float GetAngleFromDirection(EnemyAI.Direction direction)
    {
        switch (direction)
        {
            case EnemyAI.Direction.Up:
                return transform.eulerAngles.z;
            case EnemyAI.Direction.Down:
                return transform.eulerAngles.z + 180f;
            case EnemyAI.Direction.Left:
                return transform.eulerAngles.z + 90f;
            case EnemyAI.Direction.Right:
                return transform.eulerAngles.z - 90f;
            default:
                return transform.eulerAngles.z;
        }
    }
}


