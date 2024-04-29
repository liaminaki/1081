using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryPoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {   
        int isOld = PlayerPrefs.GetInt("IsOld", 0);
        
        if (isOld == 1)
        {
            SceneManager.LoadScene("MainMenuScene");
        }

    }

    public void GoTo(string SceneName) {
        SceneManager.LoadScene(SceneName);
    }
    
}
