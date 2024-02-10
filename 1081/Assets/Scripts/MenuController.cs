using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject  _mainMenuAnim;

    public void ToRight() {
        
        if (_mainMenuAnim != null) {
            // Play the specified animation
            _mainMenuAnim.GetComponent<Animator>().Play("ToRight");
        }
        
        else {
            Debug.LogWarning("Animation component missing.");
        }
    
    }

    public void ToLeft() {
        
        if (_mainMenuAnim != null) {
            // Play the specified animation
            _mainMenuAnim.GetComponent<Animator>().Play("ToLeft");
        }
        
        else {
            Debug.LogWarning("Animation component missing.");
        }
    }


}
