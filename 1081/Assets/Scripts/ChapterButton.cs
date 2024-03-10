using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ChapterButton : MonoBehaviour {

    [SerializeField] private Button _button;
    [SerializeField] private bool _unlocked; // Default value is false
    public GameObject QuestionMark; // Indicate if chapter is locked
    public GameObject[] Stars;
    public int ChapterNum;
    public Sprite FilledStar;


    private void Start() {
        // PlayerPrefs.DeleteKey("Chapter1Stars");
        // PlayerPrefs.DeleteKey("Chapter2Stars");
        // PlayerPrefs.DeleteKey("Chapter3Stars");
        // PlayerPrefs.DeleteKey("Chapter4Stars");
        // PlayerPrefs.DeleteKey("Chapter5Stars");
        // PlayerPrefs.DeleteKey("ChaptersUnlocked");
       
        UpdateChapterStatus();
        UpdateChapterButton(); 
    }

    private void UpdateChapterStatus() {
        
        //if the current lv is 5, the pre should be 4
        int prevChapterNum = ChapterNum - 1;

        // Unlock chapter only if previous chapter stars is at least one
        if (PlayerPrefs.GetInt("Chapter" + prevChapterNum + "Stars") > 0 || ChapterNum == 1) {
            UnlockChapter();
        }

        else {
            LockChapter();
        }

    }

    private void UpdateChapterButton() {

        // Check if locked
        if(!_unlocked) {   
            
            QuestionMark.SetActive(true);

            for(int i = 0; i < Stars.Length; i++) {
                Stars[i].SetActive(false);
            }
        }
        
        // If playable
        else {
            
            QuestionMark.SetActive(false);
            
            //  Show stars
            for (int i = 0; i < Stars.Length; i++) {
                Stars[i].SetActive(true);
            }

            //  Display score in terms of number of filled stars
            
            for(int i = 0; i < PlayerPrefs.GetInt("Chapter" + ChapterNum + "Stars"); i++) {
                Stars[i].GetComponent<Image>().sprite = FilledStar;
            }

            /* 
                No code to revert back to default since numbers of stars will only update if
                score is greater than previously saved number of stars
            */

        }
    }

    public void UnlockChapter() {
        _unlocked = true;
        _button.Activate();
        _button.SetInteractable(true);
    }

    public void LockChapter() {
        _unlocked = false;
        _button.Deactivate();
        _button.SetInteractable(false);
    }

    // Move to the corresponding Scene to play when chapter button is clicked
    public void GoTo(string chapterNumber) {

        if(_unlocked) {
            SceneManager.LoadScene(chapterNumber);
        }

    }

}