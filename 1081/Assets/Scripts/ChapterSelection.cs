using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChapterSelection : MonoBehaviour {

    [SerializeField] private Animator _chapterSelectionAnimator;
    private int chaptersUnlocked;
    private int newChapterUnlocked;

    // Start is called before the first frame update
    void Start() {
        
        LoadNewChapterUnlocked();
        LoadChaptersUnlocked();

        if (chaptersUnlocked == 0) {
            chaptersUnlocked = 1;
            PlayerPrefs.SetInt("ChaptersUnlocked", 1);

            _chapterSelectionAnimator.Play("Strings0-1");
        }

        // Play strings animation 
        if (newChapterUnlocked == 1) {

            _chapterSelectionAnimator.SetInteger("ChaptersUnlocked", chaptersUnlocked-1);
            _chapterSelectionAnimator.SetInteger("UnlockChapter", chaptersUnlocked);

            // Reset newChapterUnlocked
            PlayerPrefs.SetInt("NewChapterUnlocked", 0);

        }

        else if (chaptersUnlocked > 0) {
            _chapterSelectionAnimator.SetInteger("ChaptersUnlocked", chaptersUnlocked);
        }

        // Play transition if not from chapter selection
        if (SceneStateManager.PreviousScene == "MainMenuScene") {
           _chapterSelectionAnimator.Play("FromMenu");
        }

        // Clear the previous scene information to prevent it from persisting across scenes
        SceneStateManager.PreviousScene = null;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackToMenu() {
        
        SceneStateManager.PreviousScene = "ChapterSelectionScene";
        // _chapterSelectionAnimator.SetTrigger("BackToMenu");
        _chapterSelectionAnimator.Play("ToMenu");

        // Start a coroutine to wait until the animation is complete.
        StartCoroutine(WaitForAnimation());

    }

    private IEnumerator WaitForAnimation() {
        
        // Wait until the end of the current frame
        yield return new WaitForEndOfFrame();

        // Wait until the specified animation is complete
        yield return new WaitForSeconds(_chapterSelectionAnimator.GetCurrentAnimatorStateInfo(0).length);
        
        // Back to main menu
        SceneManager.LoadScene("MainMenuScene");

    }

    // Load numbers of chapters unlocked
    private void LoadChaptersUnlocked() {
        
        chaptersUnlocked = PlayerPrefs.GetInt("ChaptersUnlocked");

    }

    private void LoadNewChapterUnlocked() {
        
        newChapterUnlocked = PlayerPrefs.GetInt("NewChapterUnlocked");

    }


}
