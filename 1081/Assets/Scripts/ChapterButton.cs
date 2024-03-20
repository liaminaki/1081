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

    private Animator qMarkAnimator;
    private int isNewChapterUnlocked;
    private int chaptersUnlocked;

    private void Start() {
        // PlayerPrefs.DeleteKey("Chapter1Stars");
        // PlayerPrefs.DeleteKey("Chapter2Stars");
        // PlayerPrefs.DeleteKey("Chapter3Stars");
        // PlayerPrefs.DeleteKey("Chapter4Stars");
        // PlayerPrefs.DeleteKey("Chapter5Stars");
        // PlayerPrefs.DeleteKey("ChaptersUnlocked");
        // PlayerPrefs.DeleteKey("IsNewChapterUnlocked");
        // PlayerPrefs.DeleteKey("NewChapterUnlocked");

        // Init values
        isNewChapterUnlocked = PlayerPrefs.GetInt("IsNewChapterUnlocked");
        chaptersUnlocked = PlayerPrefs.GetInt("ChaptersUnlocked");

        qMarkAnimator = QuestionMark.GetComponent<Animator>();
       
        UpdateChapterStatus();
        UpdateChapterButton(); 

    }

    private void UpdateChapterStatus() {

        // Debug.Log("isNewChapterUnlocked: " + isNewChapterUnlocked);
        // Debug.Log("chaptersUnlocked" + chaptersUnlocked);
        // Debug.Log("chaptersNum" + ChapterNum);


        if (!IsTheNewChapterUnlocked()) {
            Debug.Log("Update Chapter Status");
            
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

        else 
            LockChapter();

    }

    public void UpdateChapterButton() {

        // Check if this is not the newly unlocked chapter
        if (!IsTheNewChapterUnlocked()) {

            Debug.Log("Update Chapter Button");

            // Check if locked
            if(!_unlocked) {   
                
                QuestionMark.SetActive(true);

                for(int i = 0; i < Stars.Length; i++) {
                    Stars[i].SetActive(false);
                }
            }
            
            // If playable
            else {
                
                // UnlockChapter();

            }

        }

        // Check if a new chapter is unlocked and the new chapter is this same chapter
        else {
            
            RemoveQMarkThenUnlock();
            
        }
            
    }

    public void RemoveQMarkThenUnlock() {
        // Wait for the strings animation to end
        // Play disappearing question mark animation
        qMarkAnimator.SetTrigger("Unlocked");

        // Wait for the qmark animation to end
        StartCoroutine(WaitForQMarkAnimation());
        
        if (chaptersUnlocked == 2)
            // Reset isNewChapterUnlocked
            PlayerPrefs.SetInt("IsNewChapterUnlocked", 0);
    }

    private IEnumerator WaitForQMarkAnimation() {
        
        // Wait until the end of the current frame
        yield return new WaitForEndOfFrame();

        // Wait until the specified animation is complete
        yield return new WaitForSeconds(2f);
        
        // QuestionMark.SetActive(false);

        // Unlock Chapter
        UnlockChapter();

    }

    public void UnlockChapter() {
        _unlocked = true;
        _button.Activate();
        _button.SetInteractable(true);

        QuestionMark.SetActive(false);
                
        //  Show stars
        for (int i = 0; i < Stars.Length; i++) {
            Stars[i].SetActive(true);
        }

        //  Display score in terms of number of filled stars
        
        for(int i = 0; i < PlayerPrefs.GetInt("Chapter" + ChapterNum + "Stars"); i++) {
            Stars[i].GetComponent<SpriteRenderer>().sprite = FilledStar;
        }

        /* 
            No code to revert back to default since numbers of stars will only update if
            score is greater than previously saved number of stars
        */
    }

    public void LockChapter() {
        _unlocked = false;
        _button.Deactivate();
        _button.SetInteractable(false);

        QuestionMark.SetActive(true);

        for(int i = 0; i < Stars.Length; i++) {
            Stars[i].SetActive(false);
        }
    }

    // Move to the corresponding Scene to play when chapter button is clicked
    public void GoTo(string chapterNumber) {

        if(_unlocked) {
            SceneManager.LoadScene(chapterNumber);
        }

    }

    // Check if this chapter is the newly unlocked chapter
    private bool IsTheNewChapterUnlocked() {

        return isNewChapterUnlocked == 1 && chaptersUnlocked == ChapterNum;

    }

}