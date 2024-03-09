using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Chapter : MonoBehaviour {

    private int currentStarsNum = 0;
    public int ChapterNum;

    // Go back to chapter selection scene
    public void OnBackClick() {
        SceneManager.LoadScene("ChapterSelectionScene");
    }

    public void PressStarsButton(int starsNum) {
        
        // PlayerPrefs.SetInt("Chapter" + ChapterNum + "Stars", 0);

        currentStarsNum = starsNum;

        // Saves only if current stars number is greater that saved stars number
        if(currentStarsNum > PlayerPrefs.GetInt("Chapter" + ChapterNum + "Stars"))
        {   
            // Save stars number
            PlayerPrefs.SetInt("Chapter" + ChapterNum + "Stars", starsNum);
        }

        Debug.Log(PlayerPrefs.GetInt("Chapter" + ChapterNum + "Stars", starsNum));

        // OnBackClick();
    }

}
