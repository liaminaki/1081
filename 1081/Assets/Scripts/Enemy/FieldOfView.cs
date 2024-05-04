using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
    public float radius = 5f;
    [Range(1, 360)] public float angle = 180f;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;

    public GameObject playerRef;
    public PlayerManager playerManager;

    public bool CanSeePlayer { get; private set; }

    public float meshResolution;
    public MeshFilter viewMeshFilter;
    Mesh viewMesh;

    public bool? idleScan;

    private EnemyAI enemyAI;

    void Start()
    {
        viewMesh = new Mesh();
        viewMesh.name = "View Mesh";
        viewMeshFilter.mesh = viewMesh;
        enemyAI = GetComponent<EnemyAI>();
        playerManager = playerRef.GetComponent<PlayerManager>();

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

    void LateUpdate(){
        DrawFieldOfView();
    }

    private void FOV()
    {
        //Gets the current character selected
        GameObject selectedCharacter = playerManager.playerPrefabs[playerManager.characterIndex];
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        if (rangeCheck.Length > 0)
        {
            foreach (var collider in rangeCheck)
            {
                if (collider.gameObject == selectedCharacter){
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
        }
        else if (CanSeePlayer)
            CanSeePlayer = false;
    }

    void DrawFieldOfView(){
        int stepCount = Mathf.RoundToInt (angle * meshResolution);
        float stepAngleSize = angle / stepCount;

        List<Vector2> viewPoints = new List<Vector2> ();

        // Get the angle based on the enemy's direction
        float startingAngle = -GetAngleFromDirection(enemyAI.CurrentDirection) - angle / 2;
        if (idleScan == true){
            startingAngle += 15f;
        }
        else if (idleScan == false){
            startingAngle -= 30f;
        }
            
        for (int i = 0; i <= stepCount; i++){
            float viewAngle = startingAngle + stepAngleSize * i;
            ViewCastInfo newViewCast = ViewCast(viewAngle);
            viewPoints.Add(newViewCast.point);
            // Debug.DrawLine(transform.position, transform.position + (Vector3)DirectionFromAngle(viewAngle) * radius, Color.red);
        }

        int vertexCount = viewPoints.Count + 1;
        Vector3[] vertices = new Vector3[vertexCount];
        int[] triangles = new int[(vertexCount-2) * 3];

        vertices [0] = Vector3.zero;
        for (int i = 0; i < vertexCount - 1; i++){
            vertices [i + 1] = transform.InverseTransformPoint(viewPoints [i]);

            if (i < vertexCount - 2){
                triangles [i * 3] = 0;
                triangles [i * 3 + 1] = i + 1;
                triangles [i * 3 + 2] = i + 2;
            }
        }

        viewMesh.Clear();
        viewMesh.vertices = vertices;
        viewMesh.triangles = triangles;
        viewMesh.RecalculateNormals ();
    }

    ViewCastInfo ViewCast (float globalAngle){
        Vector2 dir = DirectionFromAngle (globalAngle);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, radius, obstructionLayer);

        if (hit.collider!= null){
            return new ViewCastInfo (true, hit.point, hit.distance, globalAngle);
        }
        else{
           return new ViewCastInfo(false, (Vector2)transform.position + dir * radius, radius, globalAngle);
        }
    }

    public struct ViewCastInfo{
        public bool hit;
        public Vector2 point;
        public float dst;
        public float viewAngle;

        public ViewCastInfo (bool _hit, Vector2 _point, float _dst, float _viewAngle){
            hit = _hit;
            point = _point;
            dst = _dst;
            viewAngle = _viewAngle;
        }
    }

    private void OnDrawGizmos()
    {
        #if UNITY_EDITOR
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
            GameObject selectedCharacter = playerManager.playerPrefabs[playerManager.characterIndex];
            PlayerMovement playerMovement = selectedCharacter.GetComponent<PlayerMovement>();
            Gizmos.DrawLine(transform.position, playerMovement.center.position);
        }
        #endif
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


