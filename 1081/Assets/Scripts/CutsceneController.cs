using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneController : MonoBehaviour
{   
    public int CurrentCutScnId;
    private int cutscenesPlayed;
    public UnityEngine.UI.Button SkipButton;
    private bool firstTimePlaying = false;

    void Start() {

        if (SkipButton != null)
            SkipButton.gameObject.SetActive(false);
        
        LoadCutscenesPlayed();
        StartCoroutine(AddSkipButton());
    }
    
    private void LoadCutscenesPlayed() 
    {
        if (PlayerPrefs.HasKey("CutscenesPlayed")) 
            cutscenesPlayed = PlayerPrefs.GetInt("CutscenesPlayed");

    }

    private IEnumerator AddSkipButton() {
        
        // Delay for 2 seconds
        yield return new WaitForSeconds(2);

        Debug.Log(cutscenesPlayed);
        Debug.Log(CurrentCutScnId);
        
        // Check if current scene has already been played
        if (cutscenesPlayed >= CurrentCutScnId)
            SkipButton.gameObject.SetActive(true);

        else 
        {   
            if (SkipButton != null)
                SkipButton.gameObject.SetActive(false);
            firstTimePlaying = true;
        }
            
    }

    // Increment cutscenes played
    // Must be called at the end of cutscene timeline
    public void IncrCutScenesPlayed() {
        
        // Increment only if cutscene is played for the first time
        if (firstTimePlaying) 
        {
            cutscenesPlayed += 1;
            PlayerPrefs.SetInt("CutscenesPlayed", cutscenesPlayed);
        }
        
    }
    
    // Function to load a scene by name
    public void GoTo(string scene)
    {   
        SceneManager.LoadScene(scene);
    }

}