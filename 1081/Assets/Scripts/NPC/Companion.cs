using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Companion : MonoBehaviour
{
   public NavMeshAgent ai;
   public Transform player;
   public Animator aiAnim;
   Vector2 dest;

   void Update(){
    dest = player.position;
    ai.destination = dest;

    if (!ai.pathPending){
        if (ai.remainingDistance <= ai.stoppingDistance){
            aiAnim.ResetTrigger("isIdle");
            aiAnim.SetTrigger("isWalk");
        }
    }
    else{
        aiAnim.ResetTrigger("isWalk");
        aiAnim.SetTrigger("isIdle");
    }
   }
}
