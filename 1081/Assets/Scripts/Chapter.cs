using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Chapter : MonoBehaviour {

    private int currentStarsNum = 0;
    public int ChapterNum;

    void Start() {
        // Set main menu as previous scene
        SceneStateManager.PreviousScene = "GameScene";
    }

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
            // Debug.Log("old chaptersUnlocked when pressed" + chaptersUnlocked);

            if (chaptersUnlocked < 5) {
                PlayerPrefs.SetInt("ChaptersUnlocked", ++chaptersUnlocked);
                NewChapterUnlocked();
            }
               
            // Debug.Log(ChapterNum);
            // Debug.Log("new chaptersUnlocked when pressed" + PlayerPrefs.GetInt("ChaptersUnlocked"));
        }

        // Saves only if current stars number is greater that saved stars number
        if(currentStarsNum > prevStarsNum)
        {   
            // Save stars number
            PlayerPrefs.SetInt("Chapter" + ChapterNum + "Stars", starsNum);
        }

        OnBackClick();
    }

    // Indicate that there is new chapter that is unlocked
    public void NewChapterUnlocked() {

        PlayerPrefs.SetInt("IsNewChapterUnlocked", 1);
        
    }

}
