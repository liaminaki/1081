using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ChapterSelection : MonoBehaviour {

    [SerializeField] private Animator _chapterSelectionAnimator;
    private int chaptersUnlocked;

    // Start is called before the first frame update
    void Start() {

        LoadChaptersUnlocked();

        if (chaptersUnlocked == 0)
            PlayerPrefs.SetInt("ChaptersUnlocked", 1);
        
        // Debug.Log(chaptersUnlocked);

        _chapterSelectionAnimator.SetInteger("ChaptersUnlocked", chaptersUnlocked);

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


}
