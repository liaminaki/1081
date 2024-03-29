using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyBind : MonoBehaviour
{
    public InputActionReference MoveRef, SprintRef, ShieldRef;

    void Start(){

    }

    private void OnEnable(){
        MoveRef.action.Disable();
        SprintRef.action.Disable();
        ShieldRef.action.Disable();
    }

    private void OnDisable(){
        MoveRef.action.Enable();
        SprintRef.action.Enable();
        ShieldRef.action.Enable();
    }

    void Update(){

    }
}
