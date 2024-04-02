using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Shield : MonoBehaviour
{
    public Animator animator;

    public int ShieldID; 

    void Start(){ 
        animator = GetComponent<Animator>();
        // ResetShieldLevel();
        int shieldLevel = PlayerPrefs.GetInt("ShieldLevel", 1); // Get the shield level from PlayerPrefs with default value 0
        if(shieldLevel == 1){
            ChangeAnimation("ShieldLevel1"); 
        }
        else if(shieldLevel == 2){
            ChangeAnimation("ShieldLevel2"); 
        }
        else if(shieldLevel == 3){
            ChangeAnimation("ShieldLevel3"); 
        }
        else if(shieldLevel == 4){
            ChangeAnimation("ShieldLevel4"); 
        }
        else if(shieldLevel == 5){
            ChangeAnimation("ShieldLevel5"); 
        }
    }

    public void ChangeAnimation(string animationName)
    {
        if (animator != null)
        {
            animator.Play(animationName); // Play the specified animation
        }
        else
        {
            Debug.LogWarning("Animator component not found!");
        }
    }

    
    public int getShieldID(){
        return ShieldID;
    }

    public void ResetShieldLevel(){
        PlayerPrefs.SetInt("ShieldLevel", 1);
        PlayerPrefs.Save();
    }
    //  public void IncreaseShieldLevel()
    // {
    //     int shieldLevel = PlayerPrefs.GetInt("playerShieldLevelKey", 0); // Get the current shield level from PlayerPrefs
    //     shieldLevel++; // Increase the shield level
    //     PlayerPrefs.SetInt("ShieldLevel", shieldLevel); // Save the new shield level to PlayerPrefs
    //     PlayerPrefs.Save(); // Save PlayerPrefs data
    //     Debug.Log("Increase");
    // }

}
