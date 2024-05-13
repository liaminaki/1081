using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputWaiting : MonoBehaviour
{
    public GameObject editBind;
    public GameObject rebindOverlay;

    void Start(){
        editBind.SetActive(false);
    }
    void Update(){
        if (rebindOverlay.activeSelf){
            editBind.SetActive(true);
        }
        else{
            editBind.SetActive(false);
        }
    }
}
