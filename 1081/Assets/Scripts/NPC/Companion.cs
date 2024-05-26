using Pathfinding;
using UnityEngine;

public class Companion : MonoBehaviour
{
    public AIPath aiPath;
    Animator aiAnim;
    Vector2 direction;
    bool isMoving;


    void Start(){
        aiAnim = GetComponent<Animator>();
    }
    void Update(){
        FaceVelocity();
        CheckMovement();
    }

    void FaceVelocity(){
        direction = aiPath.desiredVelocity;
        aiAnim.SetFloat("X", direction.x);
        aiAnim.SetFloat("Y", direction.y);
    }

    void CheckMovement(){
        isMoving = direction.sqrMagnitude > 0.01f;
        aiAnim.SetBool("isWalk", isMoving);
    }
}
