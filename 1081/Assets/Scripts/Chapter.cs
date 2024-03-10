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
        int prevStarsNum = PlayerPrefs.GetInt("Chapter" + ChapterNum + "Stars");

        // Increment number of chapters unlocked
        // Condition indicates this is farthest unlocked chapter being played
        if (prevStarsNum == 0 && currentStarsNum > 0) {
            
            int chaptersUnlocked = PlayerPrefs.GetInt("ChaptersUnlocked");
            Debug.Log("old chaptersUnlocked" + chaptersUnlocked);
            PlayerPrefs.SetInt("ChaptersUnlocked", ++chaptersUnlocked);
            
            Debug.Log(ChapterNum);
            Debug.Log("new chaptersUnlocked" + PlayerPrefs.GetInt("ChaptersUnlocked"));
        }

        // Saves only if current stars number is greater that saved stars number
        if(currentStarsNum > prevStarsNum)
        {   
            // Save stars number
            PlayerPrefs.SetInt("Chapter" + ChapterNum + "Stars", starsNum);
        }

        // OnBackClick();
    }

}
