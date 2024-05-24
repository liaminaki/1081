using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ContinueAudio : MonoBehaviour
{
    public string musicTag;
    public List<string> scenesWithThisMusic;
    
    private void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        // Perform initial check when the game object is first created
        // CheckScene(SceneManager.GetActiveScene().name);

        DontDestroyOnLoad(this.gameObject);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string sceneName = scene.name;

        GameObject[] musicObj = GameObject.FindGameObjectsWithTag(musicTag);

        Debug.Log("Number of objects with tag " + musicTag + ": " + musicObj.Length);
        Debug.Log("Current scene: " + sceneName);
        Debug.Log("Number of objects with tag " + musicTag + ": " + musicObj.Length);

        foreach (GameObject obj in musicObj)
        {
            Debug.Log("Object name: " + obj.name);
        }

        if (!scenesWithThisMusic.Contains(sceneName)) 
        {
            Debug.Log("Scene " + sceneName + " does not have this music.");
            Destroy(gameObject);
        }

        if (musicObj.Length > 1)
        {   
                     
            // Choose the first GameObject in the array as the primary audio object
            GameObject primaryAudioObj = musicObj[0];

            Debug.Log("Primary audio object: " + primaryAudioObj.name);

            // Destroy the script's GameObject if it's not the primary audio object
            if (gameObject != primaryAudioObj)
            {
                Debug.Log("Destroying secondary audio object: " + gameObject.name);
                Destroy(gameObject);
            }
            
        }
    }

        
}